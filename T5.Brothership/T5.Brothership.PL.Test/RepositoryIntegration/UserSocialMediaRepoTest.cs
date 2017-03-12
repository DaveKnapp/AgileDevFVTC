using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using T5.Brothership.PL.Repositories;
using System.Linq;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class UserSocialMediaRepoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext = new brothershipEntities())
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_ActualEqualsExpectedData()
        {
            var expectedUserSocialMedia = new UserSocialJunc
            {
                UserID = 3,
                SocialMediaTypeID = 1,
                URL = "youtube.com/TestUserTwo"
            };

            UserSocialJunc actualUserSocialMedia;

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                userSocialMediaRepo.Add(expectedUserSocialMedia);
                userSocialMediaRepo.SaveChanges();
                actualUserSocialMedia = userSocialMediaRepo.GetById(expectedUserSocialMedia.UserID, expectedUserSocialMedia.SocialMediaTypeID);
            }

            AssertUserSocialMediasEqual(expectedUserSocialMedia, actualUserSocialMedia);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            UserSocialJunc actualUserSocialMedia;
            var userSocialMediaToDelete = AddandGetTestUserSocialMedia();

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                userSocialMediaRepo.Delete(userSocialMediaToDelete);
                userSocialMediaRepo.SaveChanges();
                actualUserSocialMedia = userSocialMediaRepo.GetById(userSocialMediaToDelete.UserID, userSocialMediaToDelete.SocialMediaTypeID);
            }

            Assert.IsNull(actualUserSocialMedia);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var userSocialMediaToDelete = AddandGetTestUserSocialMedia();
            UserSocialJunc actualSocialMedia;

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                userSocialMediaRepo.Delete(userSocialMediaToDelete.UserID, userSocialMediaToDelete.SocialMediaTypeID);
                userSocialMediaRepo.SaveChanges();
                actualSocialMedia = userSocialMediaRepo.GetById(userSocialMediaToDelete.UserID, userSocialMediaToDelete.SocialMediaTypeID);
            }

            Assert.IsNull(actualSocialMedia);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllUserSocialMediasReturned_CountEqualActual()
        {
            const int expectedCount = 6;
            int actualCount;
            using (var UserSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                actualCount = UserSocialMediaRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAllByUser_AllUserSocialMediasReturned_CountEqualsActual()
        {
            const int expectedUserId = 1;
            const int expectedCount = 3;
            int actualCount;
            using (var userSocialMediasRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                actualCount = userSocialMediasRepo.GetAllByUser(expectedUserId).Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedUserSocialMedia = new UserSocialJunc
            {
                UserID = 2,
                SocialMediaTypeID = 1,
                URL = "instagram.com/TestUserTwo"
            };
            UserSocialJunc actualUserSocialMedia;

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                actualUserSocialMedia = userSocialMediaRepo.GetById(expectedUserSocialMedia.UserID, expectedUserSocialMedia.SocialMediaTypeID);
            }

            AssertUserSocialMediasEqual(expectedUserSocialMedia, actualUserSocialMedia);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasUSerSocialMediaUpdated_ActualDataEqualsExpected()
        {
            var expectedUserSocialMedia = new UserSocialJunc
            {
                UserID = 2,
                SocialMediaTypeID = 1,
                URL = "youtube.com/UpdatedLink"
            };
            UserSocialJunc actualUserSocialMedia;

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                userSocialMediaRepo.Update(expectedUserSocialMedia);
                userSocialMediaRepo.SaveChanges();
                actualUserSocialMedia = userSocialMediaRepo.GetById(expectedUserSocialMedia.UserID, expectedUserSocialMedia.SocialMediaTypeID);
            }

            AssertUserSocialMediasEqual(expectedUserSocialMedia, actualUserSocialMedia);
        }

        private UserSocialJunc AddandGetTestUserSocialMedia()
        {
            var userSocialMedia = new UserSocialJunc
            {
                UserID = 3,
                SocialMediaTypeID = 1,
                URL = "youtube.com/TestUserTwo"
            };

            using (var userSocialMediaRepo = new UserSocialMediaRepository(new brothershipEntities()))
            {
                userSocialMediaRepo.Add(userSocialMedia);
                userSocialMediaRepo.SaveChanges();
            }

            return userSocialMedia;
        }

        private void AssertUserSocialMediasEqual(UserSocialJunc expectedUserSocialMedia, UserSocialJunc actualUserSocialMedia)
        {
            Assert.AreEqual(expectedUserSocialMedia.UserID, actualUserSocialMedia.UserID);
            Assert.AreEqual(expectedUserSocialMedia.SocialMediaTypeID, actualUserSocialMedia.SocialMediaTypeID);
            Assert.AreEqual(expectedUserSocialMedia.URL, actualUserSocialMedia.URL);
        }
    }
}
