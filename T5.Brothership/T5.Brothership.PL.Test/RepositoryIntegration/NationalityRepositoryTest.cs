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

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_EqualsExpectedData()
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
        public void DeleteByEntity_WasDeleted_actualDataNull()
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
        public void DeleteById_WasDeleted_actualDataNull()
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
        public void GetAll_Count_EqualActual()
        {
            const int expectedCount = 1;
            int actualCount;
            using (var nationalityRepo = new NationalityRepository(new brothershipEntities()))
            {
                actualCount = nationalityRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
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
        [TestInitialize]
        public void Initialize()
        {
            var script = File.ReadAllText(FilePaths.ADD_TEST_DATA_SCRIPT_PATH);

            using (brothershipEntities dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_ActualUpdatedData_EqualsExpectedData()
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
