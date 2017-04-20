using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class UserRatingViewModel
    {
        public UserRating UserRating { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}