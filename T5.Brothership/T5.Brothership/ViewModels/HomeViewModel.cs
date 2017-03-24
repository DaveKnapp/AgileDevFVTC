using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class HomeViewModel
    {
        public List<User> RandomFeaturedUsers { get; set; }
        public List<User> RandomPopularUsers { get; set; }
    }
}