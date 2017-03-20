using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.PL.Repositories;
using T5.Brothership.Entities.Models;
using System.Linq;
using System.IO;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class NationalityRepositoryTest
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
        public void Add_ActualAddedData_ActualEqualsExpectedData()
        {
            var expectedNationality = new Nationality
            {
                Description = "Beam"
            };
            Nationality actualNationality;

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                nationalityRepo.Add(expectedNationality);
                nationalityRepo.SaveChanges();
                actualNationality = nationalityRepo.GetById(expectedNationality.ID);
            }

            AssertNationalitysEqual(expectedNationality, actualNationality);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            Nationality actualNationality;
            var typeToDelete = AddandGetTestNationality();

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                nationalityRepo.Delete(typeToDelete);
                nationalityRepo.SaveChanges();
                actualNationality = nationalityRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualNationality);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var typeIdToDelete = AddandGetTestNationality().ID;
            Nationality actualNationality;

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                nationalityRepo.Delete(typeIdToDelete);
                nationalityRepo.SaveChanges();
                actualNationality = nationalityRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualNationality);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllNationalitiesReturned_CountEqualsActual()
        {
            const int expectedCount = 3;
            int actualCount;
            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                actualCount = nationalityRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedNationality = new Nationality
            {
                ID = 1,
                Description = "US and A"
            };
            Nationality actualNationality;

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                actualNationality = nationalityRepo.GetById(expectedNationality.ID);
            }

            AssertNationalitysEqual(expectedNationality, actualNationality);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasNationalityUpdated_ActualEqualsExpectedData()
        {
            var expectedNationality = new Nationality
            {
                ID = 1,
                Description = "Beam"
            };
            Nationality actualNationality;

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                nationalityRepo.Update(expectedNationality);
                nationalityRepo.SaveChanges();
                actualNationality = nationalityRepo.GetById(expectedNationality.ID);
            }

            AssertNationalitysEqual(expectedNationality, actualNationality);
        }

        private Nationality AddandGetTestNationality()
        {
            var nationalityType = new Nationality
            {
                Description = "TestType"
            };

            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                nationalityRepo.Add(nationalityType);
                nationalityRepo.SaveChanges();
            }

            return nationalityType;
        }

        private void AssertNationalitysEqual(Nationality expected, Nationality actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Description, actual.Description);
        }

    }
}
