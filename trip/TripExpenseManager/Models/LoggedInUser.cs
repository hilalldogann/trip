using System.Text.Json;

namespace TripExpenseManager.Models
{
    public readonly record struct LoggedInUser(int Id, string Name)
    {
        public string ToJson() =>
            JsonSerializer.Serialize(this);

        public static LoggedInUser LoadFromJson(string? json) =>
            string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<LoggedInUser>(json);
    }
}

