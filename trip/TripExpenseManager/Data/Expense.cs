using System.ComponentModel.DataAnnotations;
using SQLite;
using MaxLengthAttribute = System.ComponentModel.DataAnnotations.MaxLengthAttribute;

namespace TripExpenseManager.Data
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public int TripId { get; set; }

        [Required, MaxLength(100)]
        public string For { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter valid amount")]
        public double Amount { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime? SpentOn { get; set; }
    }
}

