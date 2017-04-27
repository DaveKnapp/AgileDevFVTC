using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.Entities.Models
{
    public class IntegrationInfo
    {
        public string UserLiveStreamURL { get; set; }
        public bool IsUserLive { get; set; }
        public IntegrationType.IntegrationTypes IntegrationType { get; set; }
    }
}
