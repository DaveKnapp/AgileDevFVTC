using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;

namespace T5.Brothership.PL.Test.FakeRepositories
{
    internal class UserSocialMediaFakeRepository : IUserSocialMediaRepository
    {
        List<UserSocialJunc> _fakeUserSocialMedias = new List<UserSocialJunc>();

        public UserSocialMediaFakeRepository()
        {
            InitializeFakeUserSocialMedias();
        }

        public void Add(UserSocialJunc entity)
        {
            _fakeUserSocialMedias.Add(entity);
        }

        public void Delete(UserSocialJunc entity)
        {
            var userIntegration = _fakeUserSocialMedias.FirstOrDefault(p => p.UserID == entity.UserID &&
                                                                    p.SocialMediaTypeID == entity.SocialMediaTypeID);
            _fakeUserSocialMedias.Remove(userIntegration);
        }

        public void Delete(int userId, int socialMediaTypeId)
        {
            var userIntegration = _fakeUserSocialMedias.FirstOrDefault(p => p.UserID == userId &&
                                                            p.SocialMediaTypeID == socialMediaTypeId);
            _fakeUserSocialMedias.Remove(userIntegration);
        }

        public IQueryable<UserSocialJunc> GetAll()
        {
            return _fakeUserSocialMedias.AsQueryable();
        }

        public IQueryable<UserSocialJunc> GetAllByUser(int userId)
        {
            return _fakeUserSocialMedias.Where(p => p.UserID == userId).AsQueryable();
        }

        public UserSocialJunc GetById(int userId, int socialMediaTypeId)
        {
            return _fakeUserSocialMedias.FirstOrDefault(p => p.UserID == userId && p.SocialMediaTypeID == socialMediaTypeId);
        }

        public void SaveChanges()
        {
        }

        public void Update(UserSocialJunc entity)
        {
            int entityIndex = _fakeUserSocialMedias.IndexOf(entity);
            _fakeUserSocialMedias[entityIndex] = entity;
        }

        private void InitializeFakeUserSocialMedias()
        {
            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                 UserID = 1,
                 SocialMediaTypeID = 1,
                 URL = "youtube.com/channel/TestUserOne"
            });

            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                UserID = 1,
                SocialMediaTypeID = 2,
                URL = "twitter.com/TestUserOne"
            });

            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                UserID = 1,
                SocialMediaTypeID = 3,
                URL = "instagram.com/TestUserOne"
            });

            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                UserID = 2,
                SocialMediaTypeID = 1,
                URL = "instagram.com/TestUserTwo"
            });

            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                UserID = 2,
                SocialMediaTypeID = 2,
                URL = "instagram.com/TestUserTwo"
            });

            _fakeUserSocialMedias.Add(new UserSocialJunc
            {
                UserID = 3,
                SocialMediaTypeID = 3,
                URL = "instagram.com/TestUserThree"
            });
        }
    }
}
