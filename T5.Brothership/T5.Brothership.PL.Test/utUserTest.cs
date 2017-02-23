using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL;
using System.Linq;

namespace T5.Brothership.PL.Test
{
    [TestClass]
    public class utUserTest
    {
        [TestMethod]
        public void LoadUserLoginTest()
        {
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to select user login info
            var UserLogin = (from u in oDc.Users
                             join ul in oDc.UserLogins on u.ID equals ul.UserID
                             where u.UserName == "TestUserOne"
                            select new
                            {
                                u.UserName,
                                ul.Password
                            }).FirstOrDefault();

            //checks the password based on username
            Assert.AreEqual(UserLogin.Password, "Password");
        }

        [TestMethod]
        public void InsertUserLoginTest()
        {
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to create a new user
            var UserStart = (from u in oDc.Users
                                 join ul in oDc.UserLogins on u.ID equals ul.UserID                            
                                 select new
                                 {
                                     u,
                                     ul
                                 }).ToList();

            User newUser = new User();
            UserLogin newUserLogin = new UserLogin();

            //newUser.ID = -1;
            //newUserLogin.UserID = -1;
            newUser.UserName = "UnitTestUser";
            newUserLogin.Password = "UnitTestPassword";
            newUser.Email = "UnitTest@email";
            newUser.DateJoined = DateTime.Now;
            newUser.DOB = DateTime.Parse("01/01/1990");
            newUser.Bio = "UnitTestBio";
            newUser.ProfileImagePath = "UnitTestPath";
            newUser.Gender = "U";
            //Add nationality

            //Insert it all then test each individually
            oDc.Users.Add(newUser);
            oDc.UserLogins.Add(newUserLogin);
            oDc.SaveChanges();

            var UserEnd = (from u in oDc.Users
                           join ul in oDc.UserLogins on u.ID equals ul.UserID
                           select new
                           {
                               u,
                               ul
                           }).ToList();

            Assert.AreEqual(UserStart.Count + 1, UserEnd.Count);
        }

        [TestMethod]
        public void DeleteUserLoginTest()
        {
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to select user login info
            var User = (from u in oDc.Users                             
                        where u.UserName == "UnitTestUser"
                        select u).FirstOrDefault();

            var Login = (from ul in oDc.UserLogins
                        where ul.Password == "UnitTestPassword"
                        select ul).FirstOrDefault();


            if (User != null && Login != null)
            {
                oDc.Users.Remove(User);
                oDc.UserLogins.Remove(Login);
                oDc.SaveChanges();

                var UserDeleted = (from u in oDc.Users
                            where u.UserName == "UnitTestUser"
                            select u).FirstOrDefault();

                var LoginDeleted = (from ul in oDc.UserLogins
                             where ul.Password=="UnitTestPassword"
                             select ul).FirstOrDefault();

                Assert.IsNull(UserDeleted);
                Assert.IsNull(LoginDeleted);
            }
            else
            {
                Assert.Fail("Record to delete not found");
            }
        }

        [TestMethod]
        public void InsertUserTest()
        {//this one has no referential integrity errors
            brothershipEntities oDc = new brothershipEntities();

            //Use LINQ to create a new user
            var UserStart = (from u in oDc.Users
                             select u).ToList();

            User newUser = new User();
            

            //newUser.ID = -1;            
            newUser.UserName = "UnitTestUser";           
            newUser.Email = "UnitTest@email";
            newUser.DateJoined = DateTime.Now;
            newUser.DOB = DateTime.Parse("01/01/1990");
            newUser.Bio = "UnitTestBio";
            newUser.ProfileImagePath = "UnitTestPath";
            newUser.Gender = "U";
            //Add nationality

            //Insert it all then test each individually
            oDc.Users.Add(newUser);            
            oDc.SaveChanges();

            var UserEnd = (from u in oDc.Users
                             select u).ToList();

            Assert.AreEqual(UserStart.Count + 1, UserEnd.Count);
        }
    }
}
