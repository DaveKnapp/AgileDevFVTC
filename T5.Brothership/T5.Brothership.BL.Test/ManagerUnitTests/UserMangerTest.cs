﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;
using T5.Brothership.PL;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    /// <summary>
    /// Summary description for UserMangerTest
    /// </summary>
    [TestClass]
    public class UserMangerTest
    {
        [TestCategory("UnitTest"), TestMethod]
        public void GetById_UserFound_ReturnsCorrectUser()
        {
            User expectedUser = new User
            {
                ID = 1,
                UserName = "TestUserOne",
                Email = "Testing123@yahoo.com",
                Bio = "This is my bio",
                ProfileImagePath = "../Images/TestUserOne/Pofile.png",
                DateJoined = new DateTime(2017, 2, 23),
                DOB = new DateTime(1988, 11, 12),
                Gender = "M",
                UserType = new UserType { ID = 1, Description = "User" },
                Nationality = new Nationality { ID = 1, Description = "US and A" },
                NationalityID = 1,
                UserTypeID = 1,
                UserLogin = new UserLogin { Password = "Password", UserID = 1 }
            };

            User actualUser;
            using (UserManager userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                actualUser = userManager.GetById(expectedUser.ID);
            }

            AssertUsersEqual(expectedUser, actualUser);
        }

        [TestCategory("UnitTest"), TestMethod]
        public void GetById_UserNotFound_ReturnsNull()
        {
            User actualUser;
            using (UserManager userManager = new UserManager(new FakeBrothershipUnitOfWork()))
            {
                actualUser = userManager.GetById(99);
            }

            Assert.IsNull(actualUser);
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
            Assert.AreEqual(expected.Nationality.ID, actual.Nationality.ID);
            Assert.AreEqual(expected.Nationality.Description, actual.Nationality.Description);
            Assert.AreEqual(expected.UserType.ID, actual.UserType.ID);
            Assert.AreEqual(expected.UserType.Description, actual.UserType.Description);
            Assert.AreEqual(expected.UserLogin.UserID, actual.UserLogin.UserID);
            Assert.AreEqual(expected.UserLogin.Password, actual.UserLogin.Password);
        }
    }
}