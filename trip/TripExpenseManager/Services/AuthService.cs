using System;
namespace TripExpenseManager.Services
{
    public class AuthService
    {
        private const string LoggedInKey = "logged-in";
        private readonly IPreferences _preferences;
        private readonly DatabaseContext _context;

        public AuthService(DatabaseContext context)
        {
            _preferences = Preferences.Default;
            _context = context;
        }

        public bool IsSignedIn => _preferences.ContainsKey(LoggedInKey);

        public LoggedInUser CurrentUser => LoggedInUser.LoadFromJson(_preferences.Get<string>(LoggedInKey, string.Empty));

        public async Task<MethodResult> SignupAsync(SignupModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Username = model.Username,
                Password = model.Password
            };

            if(await _context.AddItemAsync<User>(user))
            {
                SetUser(user);
                return MethodResult.Success();
            }
            return MethodResult.Fail(null);
        }

        public async Task<MethodResult> SigninAsync(SigninModel model)
        {
            var users = await _context.GetFileteredAsync<User>(u => u.Username == model.Username && u.Password == model.Password);
            var user = users.FirstOrDefault();

            if(user is not null)
            {
                SetUser(user);
                return MethodResult.Success();
            }

            return MethodResult.Fail("Incorrect credentials");
        }

        private void SetUser(User user)
        {
            var loggedInUser = new LoggedInUser(user.Id, user.Name);
            _preferences.Set(LoggedInKey, loggedInUser.ToJson());
        }

        public async Task ChangeNameAsync(string newName)
        {
            var dbUser = await _context.FindAsync<User>(CurrentUser.Id);
            dbUser.Name = newName;
            await _context.UpdateItemAsync(dbUser);
            SetUser(dbUser);
        }

        public async Task ChangePasswordAsync(string newPassword)
        {
            var dbUser = await _context.FindAsync<User>(CurrentUser.Id);
            dbUser.Password = newPassword;
            await _context.UpdateItemAsync(dbUser);
        }

        public void SignOut()
        {
            _preferences.Remove(LoggedInKey);
        }
    }
}

