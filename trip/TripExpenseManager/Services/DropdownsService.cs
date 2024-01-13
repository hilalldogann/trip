using System;
namespace TripExpenseManager.Services
{
    public class DropdownsService
    {
        private readonly DatabaseContext _context;

        public DropdownsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<LocationCategory[]> GetLocationCategoriesAsync()
        {
            return (await _context.GetAllAsync<LocationCategory>()).ToArray();
        }

        public string[] GetTripStatuses() => Enum.GetNames<TripStatus>();

        public async Task<string[]> GetExpenseCategoriesAsync() =>
            (await _context.GetAllAsync<ExpenseCategory>())
            .Select(e => e.Name)
            .ToArray();
    }
}

