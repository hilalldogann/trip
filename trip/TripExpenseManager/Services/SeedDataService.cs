using System;
using TripExpenseManager.Data;

namespace TripExpenseManager.Services
{
    public class SeedDataService
    {
        private readonly DatabaseContext _context;

        public SeedDataService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            var foodCategory = await _context.FindAsync<ExpenseCategory>("Yemek");

            if (foodCategory is not null)
                return; // Data seeding is done already

            var expenseCategories = new List<ExpenseCategory>
            {
                new("Giriş Ücreti"), new("Yol Masrafı"), new("Yemek"),new("Alış-veriş"), new("Diğer")
            };

            var locationCategories = new List<LocationCategory>
            {
                new LocationCategory("Sultan Ahmet", "/images/sultanahmet.jpeg"),
                    new LocationCategory("Dolmabahçe", "/images/dolmabahce.png"),
                    new LocationCategory("Topkapı", "/images/topkapı.png"),
                    new LocationCategory("Galata", "/images/galata.png"),
                    new LocationCategory("Rumeli", "/images/rumeli.jpeg"),
                    new LocationCategory("Istanbul Modern", "/images/istanbulmodern.jpeg"),
                    new LocationCategory("Pera", "/images/pera.jpeg"),
                    new LocationCategory("Panaroma1453", "/panaroma1453.jpeg"),
                    new LocationCategory("Istanbul Arkeoloji", "/arkeoloji.jpeg")
            };

            foreach(var location in locationCategories)
            {
                await _context.AddItemAsync<LocationCategory>(location);
            }

            foreach (var expenseCategory in expenseCategories)
            {
                await _context.AddItemAsync<ExpenseCategory>(expenseCategory);
            }
        }
    }
}

