using System;
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
        public double AverageRating { get; set; }
        public List<IntegrationInfo> UserIntegrationInfos { get; set; }       
    }

    //TODO(dave) Do I want to move this class?
    public class IntegrationInfo
    {
        public string UserLiveStreamURL { get; set; }
        public bool IsUserLive { get; set; }
        public IntegrationType.IntegrationTypes IntegrationType { get; set; }
    }
}