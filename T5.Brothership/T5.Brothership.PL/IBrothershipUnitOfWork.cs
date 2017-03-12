using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL
{
    public interface IBrothershipUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IGameRepository Games { get; }
        IIntegrationTypeRepository IntegrationTypes { get; }
        INationalityRepository Nationalities { get;}
        IRatingRepository Ratings { get; }
        ISocialMediaTypeRepository SocialMediaTypes { get; }
        IUserIntegrationRepository UserIntegrations { get; }
        IUserRatingRepository UserRatings { get; }
        IUserSocialMediaRepository UserSocialMedias { get; }
        IUserTypeRepository UserTypes { get; }
        IGenderRepository Genders { get; }

        void Commit();
    }
}
