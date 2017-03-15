using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using T5.Brothership.PL;
using T5.Brothership.PL.Test;
using T5.Brothership.Entities.Models;
using T5.Brothership.BL.Managers;
using System.Threading.Tasks;

namespace T5.Brothership.BL.Test.ManagerIntegration
{
    [TestClass]
    public class UserManagerTest
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
        public async Task Add_WasDataAdded_expectedDataEqualsActualData()
        {
            const string expectedUserPassword = "TestPassword";
            User expectedUser = new User
            {
                UserName = "SuperFly",
                Bio = "Hello This is me",
                DOB = new DateTime(1990, 3, 12),
                Email = "MrSuper@gmail.com",
                GenderId = 1,
                NationalityID = 1,
                UserTypeID = 1,
                ProfileImagePath = "MrFlyImagePath.jpg"
            };

            User actualUser;
            using (UserManager userManger = new UserManager())
            {
                await userManger.Add(expectedUser, expectedUserPassword);
                actualUser = userManger.GetById(expectedUser.ID); 
            }

            Assert.IsNotNull(actualUser);
            Assert.AreNotEqual(actualUser.ID, 0);
        }

        //TODO(DAVE) Add GetBy Id Test
    }
}
