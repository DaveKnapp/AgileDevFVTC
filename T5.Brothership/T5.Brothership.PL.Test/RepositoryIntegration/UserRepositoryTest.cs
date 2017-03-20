using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext =  new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME))
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }

        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void DeleteById_WasRecordDeleted_GetReturnsNull()
        {
            User actualUser;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userRepo.Delete(1);
                userRepo.SaveChanges();
                actualUser = userRepo.GetById(1);
            }

            Assert.IsNull(actualUser);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void DeleteByUser_WasRecordDeleted_GetReturnsNull()
        {
            User actualUser;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUser = userRepo.GetById(1);
                userRepo.Delete(actualUser);
                userRepo.SaveChanges();
                actualUser = userRepo.GetById(1);
            }

            Assert.IsNull(actualUser);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllUsersReturned_CountEqualsActualCount()
        {
            const int expectedCount = 5;
            int actualCount;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualCount = userRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByEmail_WasUserFound_ReturnsNotNull()
        {
            const string expextedEmail = "TestingUser3@yahoo.com";

            User actualUser;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedEmail);
            }

            Assert.IsNotNull(actualUser);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void GetByID_WasDataGot_ActualEqualsExpectedData()
        {
            var expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/profile.png",
                DateJoined = new DateTime(2017, 2, 20),
                DOB = new DateTime(1988, 11, 12),
                GenderId = 1,
                UserTypeID = 1,
                NationalityID = 1
            };

            var actualUser = new User();
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUser = userRepo.GetById(expectedUser.ID);
            }

            AssertUsersEqual(expectedUser, actualUser);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByUsername_WasUserFound_ReturnsNotNull()
        {
            const string expextedUserName = "TestUserThree";

            User actualUser;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedUserName);
            }

            Assert.IsNotNull(actualUser);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByUsernameOrEmail_NoUserFound_ReturnsNull()
        {
            const string expextedUserName = "NoUserFound";

            User actualUser;
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedUserName);
            }

            Assert.IsNull(actualUser);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void Insert_WasRecordInserted_ActualUserEqualsExpectedData()
        {
            var expectedUser = new User
            {
                Bio = "Hello This is a test",
                DateJoined = DateTime.Now,
                DOB = new DateTime(1990, 3, 2),
                Email = "expectedUser@gmail.com",
                NationalityID = 1,
                GenderId = 1,
                UserLogin = new UserLogin { PasswordHash = "PasswordTest", Salt = "none" },
                UserName = "TEstUserName",
                ProfileImagePath = "TestUserImage.png",
                UserTypeID = 1
            };

            var actualUser = new User();
            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userRepo.Add(expectedUser);
                userRepo.SaveChanges();
                actualUser = userRepo.GetById(expectedUser.ID);
            }

            AssertUsersEqual(expectedUser, actualUser);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void Update_WasUserUpdated_ActualEqualsExpectedData()
        {
            User expectedUser;
            User actualUser;

            using (UserRepository userRepo = new UserRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                expectedUser = userRepo.GetById(1);
                expectedUser.Email = "UpdatedEmail@gmail.com";
                userRepo.Update(expectedUser);
                userRepo.SaveChanges();

                actualUser = userRepo.GetById(1);
            }

            AssertUsersEqual(expectedUser, actualUser);
        }

        private void AssertUsersEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Bio, actual.Bio);
            Assert.AreEqual(expected.DateJoined, actual.DateJoined);
            Assert.AreEqual(expected.DOB, actual.DOB);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.GenderId, actual.GenderId);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
        }
    }
}
