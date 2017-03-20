using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public String Password { get; set; }
        public List<Gender> Genders { get; set; }
        public List<Nationality> Nationalities { get; set; }

    }
}