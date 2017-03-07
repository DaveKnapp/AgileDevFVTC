using System;
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
    public class UserTypeRepoTest
    {
        [TestClass]
        public class UserTypeRepositoryTest
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

            [TestMethod, TestCategory("IntegrationTest")]
            public void GetAll_Count_EqualActual()
            {
                int expectedCount = 1;
                int actualCount;

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    actualCount = userTypeRepo.GetAll().Count();
                }

                Assert.AreEqual(expectedCount, actualCount);
            }

            [TestMethod, TestCategory("IntegrationTest")]
            public void GetById_CorrectDataGot_EqualExpectedData()
            {
                UserType expectedUserType = new UserType
                {
                    ID = 1,
                    Description = "User"
                };
                UserType actualUserType;

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    actualUserType = userTypeRepo.GetById(expectedUserType.ID);
                }

                AssertUserTypesEqual(expectedUserType, actualUserType);
            }

            [TestMethod, TestCategory("IntegrationTest")]
            public void Add_ActualAddedData_EqualsExpectedData()
            {
                UserType expectedUserType = new UserType
                {
                    Description = "NewTestType"
                };
                UserType actualUserType;

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    userTypeRepo.Add(expectedUserType);
                    userTypeRepo.SaveChanges();
                    actualUserType = userTypeRepo.GetById(expectedUserType.ID);
                }

                AssertUserTypesEqual(expectedUserType, actualUserType);
            }

            [TestMethod, TestCategory("IntegrationTest")]
            public void Update_ActualUpdatedData_EqualsExpectedData()
            {
                UserType expectedUserType = new UserType
                {
                    ID = 1,
                    Description = "UpdatedUserType"
                };
                UserType actualUserType;

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    userTypeRepo.Update(expectedUserType);
                    actualUserType = userTypeRepo.GetById(expectedUserType.ID);
                }

                AssertUserTypesEqual(expectedUserType, actualUserType);
            }

            [TestMethod, TestCategory("IntegrationTest")]
            public void DeleteByEntity_WasDeleted_actualDataNull()
            {
                UserType actualUserType;
                UserType typeToDelete = AddandGetTestUserType();

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    userTypeRepo.Delete(typeToDelete);
                    userTypeRepo.SaveChanges();
                    actualUserType = userTypeRepo.GetById(typeToDelete.ID);
                }

                Assert.IsNull(actualUserType);
            }

            [TestMethod, TestCategory("IntegrationTest")]
            public void DeleteById_WasDeleted_actualDataNull()
            {
                int typeIdToDelete = AddandGetTestUserType().ID;
                UserType actualUserType;

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
                {
                    userTypeRepo.Delete(typeIdToDelete);
                    userTypeRepo.SaveChanges();
                    actualUserType = userTypeRepo.GetById(typeIdToDelete);
                }

                Assert.IsNull(actualUserType);
            }

            private UserType AddandGetTestUserType()
            {
                var userTypeType = new UserType
                {
                    Description = "TestUserType"
                };

                using (var userTypeRepo = new UserTypeRepository(new brothershipEntities()))
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
}
