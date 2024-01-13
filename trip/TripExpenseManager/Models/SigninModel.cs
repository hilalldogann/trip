using System;
using System.ComponentModel.DataAnnotations;

namespace TripExpenseManager.Models
{
    public class SigninModel
    {
        [Required, MaxLength(20)]
        public string Username { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}

