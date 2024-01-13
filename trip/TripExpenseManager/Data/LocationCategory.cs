using SQLite;

namespace TripExpenseManager.Data
{
    public class LocationCategory
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Image { get; set; }

        public LocationCategory(string name, string image)
        {
            Name = name;
            Image = image;
        }
        public LocationCategory()
        {

        }
    }
}

