using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL
{
    public class BrothershipUnitOfWork : IBrothershipUnitOfWork
    {
        //NOTE(Dave) If we have problems we might have to disable lazy loading
        private brothershipEntities dbContext = new brothershipEntities();
        private UserRepository _userRepository;
        private GameRepository _gameRepository;
        private IntegrationTypeRepository _integrationTypes;
        private NationalityRepository _nationalities;
        private RatingRepository _ratings;
        private SocialMediaTypeRepository _socialMediaTypes;
        private UserIntegrationRepository _userIntegrations;
        private UserRatingRepository _userRatings;
        private UserSocialMediaRepository _userSocialMedias;
        private UserTypeRepository _userTypes;
        private GenderRepository _genders;

        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(dbContext);
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
                    _integrationTypes = new IntegrationTypeRepository(dbContext);
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
                    _nationalities = new NationalityRepository(dbContext);
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
                    _ratings = new RatingRepository(dbContext);
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
                    _socialMediaTypes = new SocialMediaTypeRepository(dbContext);
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
                    _userIntegrations = new UserIntegrationRepository(dbContext);
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
                    _userRatings = new UserRatingRepository(dbContext);
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
                    _userRepository = new UserRepository(dbContext);
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
                    _userSocialMedias = new UserSocialMediaRepository(dbContext);
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
                    _userTypes = new UserTypeRepository(dbContext);
                }
                return _userTypes;
            }
        }

        public IGenderRepository Genders
        {
            get
            {
                if (_genders == null)
                {
                    _genders = new GenderRepository(dbContext);
                }
                return _genders;
            }
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }
        }
    }
}
