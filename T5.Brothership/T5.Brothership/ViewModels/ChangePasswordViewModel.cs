using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T5.Brothership.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password),
        StringLength(20, ErrorMessage = "Password must be less than 20 and greater than 5 characters", MinimumLength = 5)]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password),
         StringLength(20, ErrorMessage = "Password must be less than 20 and greater than 5 characters", MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match "),
         Display(Name = "Confirm Password"),
         DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}