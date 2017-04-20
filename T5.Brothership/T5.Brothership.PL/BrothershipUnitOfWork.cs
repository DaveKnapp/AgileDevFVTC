using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL
{
    public class BrothershipUnitOfWork : IBrothershipUnitOfWork
    {
        //NOTE(Dave) If we have problems we might have to disable lazy loading
        private brothershipEntities _dbContext;
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
        private AzureRepository _azureRepository;

        public BrothershipUnitOfWork()
        {
            _dbContext = new brothershipEntities();
        }

        public BrothershipUnitOfWork(brothershipEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(_dbContext);
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
                    _integrationTypes = new IntegrationTypeRepository(_dbContext);
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
                    _nationalities = new NationalityRepository(_dbContext);
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
                    _ratings = new RatingRepository(_dbContext);
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
                    _socialMediaTypes = new SocialMediaTypeRepository(_dbContext);
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
                    _userIntegrations = new UserIntegrationRepository(_dbContext);
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
                    _userRatings = new UserRatingRepository(_dbContext);
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
                    _userRepository = new UserRepository(_dbContext);
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
                    _userSocialMedias = new UserSocialMediaRepository(_dbContext);
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
                    _userTypes = new UserTypeRepository(_dbContext);
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
                    _genders = new GenderRepository(_dbContext);
                }
                return _genders;
            }
        }

        public IAzureRepository AzureBlobStorage
        {
            get
            {
                if (_azureRepository == null)
                {
                    _azureRepository = new AzureRepository();
                }
                return _azureRepository;
            }
        }

        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
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
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }
    }
}
