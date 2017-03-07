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
    /// <summary>
    /// Summary description for UserRepositoryTest
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            string script = File.ReadAllText(FilePaths.ADD_TEST_DATA_SCRIPT_PATH);

            using (brothershipEntities dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }

        }
        [TestCategory("IntegrationTest"), TestMethod]
        public void Insert_WasRecordInserted_ActualUserNotNull()
        {
            User expectedUser = new User
            {
                Bio = "Hello This is a test",
                DateJoined = DateTime.Now,
                DOB = new DateTime(1990, 3, 2),
                Email = "expectedUser@gmail.com",
                NationalityID = 1,
                Gender = "M",
                UserLogin = new UserLogin { PasswordHash = "PasswordTest", Salt= "none"},
                UserName = "TEstUserName",
                ProfileImagePath = "TestUserImage.png",
                UserTypeID = 1
            };

            User actualUser = new User();
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                userRepo.Add(expectedUser);
                userRepo.SaveChanges();
                actualUser = userRepo.GetById(expectedUser.ID);
            }

            Assert.AreEqual(expectedUser.UserName, actualUser.UserName);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void GetByID_WasDataGot_ActualUserNotNull()
        {
            User expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/profile.png",
                DateJoined = new DateTime(2017,2,20),
                DOB = new DateTime(1988,11,12),
                Gender = "M",
                UserTypeID = 1,
                NationalityID = 1
            };

            User actualUser = new User();
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser = userRepo.GetById(expectedUser.ID);
            }

            Assert.AreEqual(expectedUser.ID, actualUser.ID);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void GetAll_NumberOfRecords_EqualsActual()
        {
            int expectedNumberOfUsers = 5;

            int actualNumberOfusers;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                 actualNumberOfusers = userRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedNumberOfUsers, actualNumberOfusers);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void DeleteById_WasRecordDeleted_GetReturnsNull()
        {
            User actualUser;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
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
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser =  userRepo.GetById(1);
                userRepo.Delete(actualUser);
                userRepo.SaveChanges();
                actualUser = userRepo.GetById(1);
            }

            Assert.IsNull(actualUser);
        }

        [TestCategory("IntegrationTest"), TestMethod]
        public void Update_WasRecordUpdated_DBContainsUpdatedUser()
        {
            User expectedUser;
            User actualUser;

            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                expectedUser = userRepo.GetById(1);
                expectedUser.Email = "UpdatedEmail@gmail.com";
                userRepo.Update(expectedUser);
                userRepo.SaveChanges();

                actualUser = userRepo.GetById(1);
            }

            Assert.AreEqual(expectedUser.Email, actualUser.Email);
        }

       
        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByUsername_WasUserFound_ReturnsNotNull()
        {
            string expextedUserName = "TestUserThree";

            User actualUser;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedUserName);
            }

            Assert.IsNotNull(actualUser);
        }

        // (TH) - Added this test for email.
        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByEmail_WasUserFound_ReturnsNotNull()
        {
            string expextedEmail = "TestingUser3@yahoo.com";

            User actualUser;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedEmail);
            }

            Assert.IsNotNull(actualUser);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetByUsernameOrEmail_NoUserFound_ReturnsNull()
        {
            string expextedUserName = "NoUserFound";

            User actualUser;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedUserName);
            }

            Assert.IsNull(actualUser);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualsActualCount()
        {
            int expectedCount = 5;
            int actualCount;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualCount = userRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        private void AssertUsersEqual(User expected, User actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Bio, actual.Bio);
            Assert.AreEqual(expected.DateJoined, actual.DateJoined);
            Assert.AreEqual(expected.DOB, actual.DOB);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.NationalityID, actual.NationalityID);
            Assert.AreEqual(expected.ProfileImagePath, actual.ProfileImagePath);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.UserTypeID, actual.UserTypeID);
        }
    }
}
