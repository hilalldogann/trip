using System.ComponentModel.DataAnnotations;

namespace TripExpenseManager.Models
{
    public class SignupModel
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string Username { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}

