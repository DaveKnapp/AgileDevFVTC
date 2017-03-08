using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;
using T5.Brothership.PL.Repositories;
using T5.Brothership.PL.Test.FakeRepositories;

namespace T5.Brothership.PL.Test
{
    public class FakeBrothershipUnitOfWork : IBrothershipUnitOfWork
    {
        private UserFakeRepository _userRepository;
        private GameFakeRepository _gameRepository;
        private IntegrationTypeFakeRepository _integrationTypes;
        private NationalityFakeRepository _nationalities;
        private RatingFakeRepository _ratings;
        private SocialMediaTypeFakeRepository _socialMediaTypes;
        private UserIntegrationFakeRepository _userIntegrations;
        private UserRatingFakeRepository _userRatings;
        private UserSocialMediaFakeRepository _userSocialMedias;
        private UserTypeFakeRepository _userTypes;

        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameFakeRepository();
                }
                return _gameRepository;
            }
        }

        public IIntegrationTypeRepository IntegrationTypes
        {
            get
            {
                if (_integrationTypes == null)
                {
                    _integrationTypes = new IntegrationTypeFakeRepository();
                }
                return _integrationTypes;
            }
        }

        public INationalityRepository Nationalities
        {
            get
            {
                if (_nationalities == null)
                {
                    _nationalities = new NationalityFakeRepository();
                }
                return _nationalities;
            }
        }

        public IRatingRepository Ratings
        {
            get
            {
                if (_ratings == null)
                {
                    _ratings = new RatingFakeRepository();
                }
                return _ratings;
            }
        }

        public ISocialMediaTypeRepository SocialMediaTypes
        {
            get
            {
                if (_socialMediaTypes == null)
                {
                    _socialMediaTypes = new SocialMediaTypeFakeRepository();
                }
                return _socialMediaTypes;
            }
        }

        public IUserIntegrationRepository UserIntegrations
        {
            get
            {
                if (_userIntegrations == null)
                {
                    _userIntegrations = new UserIntegrationFakeRepository();
                }
                return _userIntegrations;
            }
        }

        public IUserRatingRepository UserRatings
        {
            get
            {
                if (_userRatings == null)
                {
                    _userRatings = new UserRatingFakeRepository();
                }
                return _userRatings;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserFakeRepository();
                }
                return _userRepository;
            }
        }

        public IUserSocialMediaRepository UserSocialMedias
        {
            get
            {
                if (_userSocialMedias == null)
                {
                    _userSocialMedias = new UserSocialMediaFakeRepository();
                }
                return _userSocialMedias;
            }
        }

        public IUserTypeRepository UserTypes
        {
            get
            {
                if (_userTypes == null)
                {
                    _userTypes = new UserTypeFakeRepository();
                }
                return _userTypes;
            }
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
          
        }
    }
}
