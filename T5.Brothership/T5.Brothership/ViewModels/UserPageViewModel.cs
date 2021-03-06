﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.ViewModels
{
    public class UserPageViewModel
    {
        public User User { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public bool IsUserFollowing { get; set; }
        public bool HasLoggedInUserRated { get; set; }
        public double AverageRating { get; set; }
        public List<IntegrationInfo> UserIntegrationInfos { get; set; }
        public List<VideoContent> RecentContent { get; set; }
    }
}