using System;
namespace TripExpenseManager.Models
{
    public readonly record struct TabbarItem(string Icon, Action OnTap);
}

