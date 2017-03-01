using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL;
using System.Diagnostics;

namespace T5.Brothership.BL.Test
{
    [TestClass]
    public class utLoginTest
    {
        [TestMethod]
        public void UserLoginTest()
        {
            CUser oUser = new CUser();
            oUser.Login("TestUserOne", "Password");
            //oUser.Load("Testing123@yahoo.com", "Password"); //Email can also be used to log in

            //Click Output under the finished test to see additional info
            Trace.WriteLine(oUser.Id);
            Trace.WriteLine(oUser.UserName);
            Trace.WriteLine(oUser.Email);
            Trace.WriteLine(oUser.Bio);
            Trace.WriteLine(oUser.ProfileImagePath);
            Trace.WriteLine(oUser.DateJoined);
            Trace.WriteLine(oUser.DOB);
            Trace.WriteLine(oUser.Gender);
            Trace.WriteLine(oUser.Password);
            Trace.WriteLine(oUser.Nationality);
            Trace.WriteLine(oUser.UserType);

            Assert.AreEqual(1, oUser.Id);

            oUser = null;
        }

        [TestMethod]
        public void CookieTest()
        {

        }
    }
}