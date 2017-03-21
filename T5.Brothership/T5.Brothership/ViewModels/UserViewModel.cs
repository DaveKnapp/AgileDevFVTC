using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class CreateUserViewModel
    {
        public User User { get; set; }

        [Required, DataType(DataType.Password),
         StringLength(20, ErrorMessage = "Password must be less than 20 and greater than 5 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords don't match "),
         Display(Name ="Confirm Password"),
         DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public List<Gender> Genders { get; set; }
        public List<Nationality> Nationalities { get; set; }

    }
}