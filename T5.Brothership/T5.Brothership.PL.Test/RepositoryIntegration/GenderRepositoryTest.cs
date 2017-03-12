using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL.Repositories;
using T5.Brothership.Entities.Models;
using System.IO;
using System.Linq;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class GenderRepositoryTest
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
        public void Add_WasGenderAdded_ActualEqualsExpectedData()
        {
            var expectedGender = new Gender
            {
                Description = "Test Gender"
            };
            Gender actualGender;

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                genderRepo.Add(expectedGender);
                genderRepo.SaveChanges();
                actualGender = genderRepo.GetById(expectedGender.Id);
            }

            AssertGendersEqual(expectedGender, actualGender);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            Gender actualGender;
            var typeToDelete = AddandGetTestGender();

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                genderRepo.Delete(typeToDelete);
                genderRepo.SaveChanges();
                actualGender = genderRepo.GetById(typeToDelete.Id);
            }

            Assert.IsNull(actualGender);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var typeIdToDelete = AddandGetTestGender().Id;
            Gender actualGender;

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                genderRepo.Delete(typeIdToDelete);
                genderRepo.SaveChanges();
                actualGender = genderRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualGender);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllGendersReturned_CountEqualsActual()
        {
            const int expectedCount = 3;
            int actualCount;
            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                actualCount = genderRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualsExpectedData()
        {
            var expectedGender = new Gender
            {
                Id = 1,
                Description = "Male"
            };
            Gender actualGender;

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                actualGender = genderRepo.GetById(expectedGender.Id);
            }

            AssertGendersEqual(expectedGender, actualGender);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasGenderUpdated_ActualEqualsExpectedData()
        {
            var expectedGender = new Gender
            {
                Id = 1,
                Description = "Updated Gender"
            };
            Gender actualGender;

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                genderRepo.Update(expectedGender);
                genderRepo.SaveChanges();
                actualGender = genderRepo.GetById(expectedGender.Id);
            }

            AssertGendersEqual(expectedGender, actualGender);
        }

        private Gender AddandGetTestGender()
        {
            var genderType = new Gender
            {
                Description = "TestGender"
            };

            using (var genderRepo = new GenderRepository(new brothershipEntities()))
            {
                genderRepo.Add(genderType);
                genderRepo.SaveChanges();
            }

            return genderType;
        }

        private void AssertGendersEqual(Gender expected, Gender actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Description, actual.Description);
        }
    }
}
