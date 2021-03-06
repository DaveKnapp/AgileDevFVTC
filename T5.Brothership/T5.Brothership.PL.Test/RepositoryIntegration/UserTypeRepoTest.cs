﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using T5.Brothership.PL.Repositories;
using T5.Brothership.Entities.Models;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{

    [TestClass]
    public class UserTypeRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var dbContext =  new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME))
            {
                SqlScriptRunner.RunAddTestDataScript(dbContext);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_WasUserTypeAdded_ActualEqualsExpectedData()
        {
            var expectedUserType = new UserType
            {
                Description = "NewTestType"
            };
            UserType actualUserType;

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userTypeRepo.Add(expectedUserType);
                userTypeRepo.SaveChanges();
                actualUserType = userTypeRepo.GetById(expectedUserType.ID);
            }

            AssertUserTypesEqual(expectedUserType, actualUserType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            UserType actualUserType;
            var typeToDelete = AddandGetTestUserType();

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userTypeRepo.Delete(typeToDelete);
                userTypeRepo.SaveChanges();
                actualUserType = userTypeRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualUserType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_actualDataIsNull()
        {
            var typeIdToDelete = AddandGetTestUserType().ID;
            UserType actualUserType;

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userTypeRepo.Delete(typeIdToDelete);
                userTypeRepo.SaveChanges();
                actualUserType = userTypeRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualUserType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllUserTypesReturned_CountEqualActual()
        {
            const int expectedCount = 2;
            int actualCount;

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualCount = userTypeRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualExpectedData()
        {
            var expectedUserType = new UserType
            {
                ID = 1,
                Description = "User"
            };
            UserType actualUserType;

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualUserType = userTypeRepo.GetById(expectedUserType.ID);
            }

            AssertUserTypesEqual(expectedUserType, actualUserType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasUserTypeUpdated_ActualEqualsExpectedData()
        {
            var expectedUserType = new UserType
            {
                ID = 1,
                Description = "UpdatedUserType"
            };
            UserType actualUserType;

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userTypeRepo.Update(expectedUserType);
                actualUserType = userTypeRepo.GetById(expectedUserType.ID);
            }

            AssertUserTypesEqual(expectedUserType, actualUserType);
        }

        private UserType AddandGetTestUserType()
        {
            var userTypeType = new UserType
            {
                Description = "TestUserType"
            };

            using (var userTypeRepo = new UserTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                userTypeRepo.Add(userTypeType);
                userTypeRepo.SaveChanges();
            }

            return userTypeType;
        }

        private void AssertUserTypesEqual(UserType expected, UserType actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Description, actual.Description);
        }

    }
}
