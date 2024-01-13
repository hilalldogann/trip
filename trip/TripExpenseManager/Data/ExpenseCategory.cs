using SQLite;
using MaxLengthAttribute = System.ComponentModel.DataAnnotations.MaxLengthAttribute;

namespace TripExpenseManager.Data
{
    public class ExpenseCategory
    {
        [PrimaryKey, MaxLengthAttribute(100)]
        public string Name { get; set; }

        public ExpenseCategory(string name)
        {
            Name = name;
        }
        public ExpenseCategory()
        {

        }
    }
}

