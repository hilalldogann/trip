using System;
namespace TripExpenseManager.Services
{
    public class TripService
    {
        private readonly DatabaseContext _context;

        public TripService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetTripsAsync(int pageNo = 1, int count = 10)
        {
            var query = await _context.GetTableAsync<Trip>();
            return await query.OrderByDescending(t => t.AddedOn)
                        .Skip((pageNo - 1) * count)
                        .Take(count)
                        .ToArrayAsync();
        }

        public async Task<MethodDataResult<Trip>> SaveTripAsync(Trip trip)
        {
            trip.Status = Enum.Parse<TripStatus>(trip.DisplayStatus);
            try
            {
                if (trip.Id == 0)
                {
                    // Create a new trip
                    await _context.AddItemAsync<Trip>(trip);
                }
                else
                {
                    // Modify an existing trip
                    await _context.UpdateItemAsync<Trip>(trip);
                }
                return MethodDataResult<Trip>.Success(trip);
            }
            catch (Exception ex)
            {
                return MethodDataResult<Trip>.Fail(ex.Message);
            }
        }

        public async Task<Trip?> GetTripAsync(int tripId, bool includeExpenses = false)
        {
            var trip = await _context.FindAsync<Trip>(tripId);
            if (includeExpenses)
            {
                trip.Expenses = await _context.GetFileteredAsync<Expense>(e => e.TripId == tripId) ?? Enumerable.Empty<Expense>();
            }
            return trip;
        }

        public async Task<MethodDataResult<Expense>> SaveExpenseAsync(Expense expense)
        {
            try
            {
                if (expense.Id == 0)
                {
                    // Add expense
                    await _context.AddItemAsync<Expense>(expense);
                }
                else
                {
                    // Modify an existing expense
                    await _context.UpdateItemAsync<Expense>(expense);
                }
                return MethodDataResult<Expense>.Success(expense);
            }
            catch (Exception ex)
            {
                return MethodDataResult<Expense>.Fail(ex.Message);
            }
        }

        public async Task<Expense?> GetExpenseAsync(long expenseId) =>
            await _context.FindAsync<Expense>(expenseId);

        public async Task<MethodResult> SaveExpenseCategoryAsync(string categoryName)
        {
            var dbCategory = await _context.FindAsync<ExpenseCategory>(categoryName);
            if(dbCategory is not null)
            {
                return MethodResult.Fail($"Kategori [{categoryName}] önceden oluşturulmuştu.");
            }
            await _context.AddItemAsync<ExpenseCategory>(new ExpenseCategory(categoryName));
            return MethodResult.Success();
        }
    }
}

