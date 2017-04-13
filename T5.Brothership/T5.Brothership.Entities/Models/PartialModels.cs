using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.ModelMetaData;

namespace T5.Brothership.Entities.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User { }

    [MetadataType(typeof(UserLoginMetaData))]
    public partial class UserLogin { }

    public partial class UserType
    {
        public enum UserTypes { User = 1, FeaturedUser =2 }
    }

    public partial class SocialMediaType
    {
        public enum SocialMediaTypes { Youtube = 1, Twitter = 2, Instagram = 3 }
    }

    public partial class IntegrationType
    {
        public enum IntegrationTypes { Twitch = 1 }
    }
}
