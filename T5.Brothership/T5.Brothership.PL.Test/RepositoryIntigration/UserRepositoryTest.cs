using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Entities.Models;
using T5.Brothership.PL.Repositories;
using System.IO;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntigration
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
        [TestCategory("IntigrationTest"), TestMethod]
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
                UserLogin = new UserLogin { PasswordHash = "PasswordTest" },
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

        [TestCategory("IntigrationTest"), TestMethod]
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

        [TestCategory("IntigrationTest"), TestMethod]
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

        [TestCategory("IntigrationTest"), TestMethod]
        public void Delete_WasRecordDeleted_GetReturnsNull()
        {
            throw new NotImplementedException();
            //TODO Fix this my adding cascading delete
            //User actualUser;
            //using (UserRepository userRepo = new UserRepository())
            //{
            //    userRepo.Delete(1);
            //    userRepo.Save();
            //    actualUser = userRepo.GetByID(1);
            //}

            //Assert.IsNull(actualUser);
        }

        [TestCategory("IntigrationTest"), TestMethod]
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

        [TestMethod, TestCategory("IntigrationTest")]
        public void GetByUsernameOrEmail_WasCorrectUserFound_ReturnsNotNull()
        {
            string expextedUserName = "TestUserThree";
            string expectedEmail = "TestingUser3@yahoo.com";

            User actualUser;
            using (UserRepository userRepo = new UserRepository(new brothershipEntities()))
            {
                actualUser = userRepo.GetByUsernameOrEmail(expextedUserName, expectedEmail);
            }

            Assert.IsNotNull(actualUser);
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
