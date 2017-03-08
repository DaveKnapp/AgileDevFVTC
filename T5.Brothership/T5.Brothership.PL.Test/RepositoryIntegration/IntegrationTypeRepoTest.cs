using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using T5.Brothership.PL.Repositories;
using T5.Brothership.Entities.Models;

namespace T5.Brothership.PL.Test.RepositoryIntegration
{
    [TestClass]
    public class IntegrationTypeRepoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            var script = File.ReadAllText(FilePaths.ADD_TEST_DATA_SCRIPT_PATH);

            using (var dataContext = new brothershipEntities())
            {
                dataContext.Database.ExecuteSqlCommand(script);
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            var expectedIntegrationType = new IntegrationType
            {
                Description = "Beam"
            };
            IntegrationType actualIntegrationType;

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                integrationTypeRepo.Add(expectedIntegrationType);
                integrationTypeRepo.SaveChanges();
                actualIntegrationType = integrationTypeRepo.GetById(expectedIntegrationType.ID);
            }

            AssertIntegrationTypesEqual(expectedIntegrationType, actualIntegrationType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIsNull()
        {
            IntegrationType actualIntegrationType;
            var typeToDelete = AddandGetTestIntegrationType();

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                integrationTypeRepo.Delete(typeToDelete);
                integrationTypeRepo.SaveChanges();
                actualIntegrationType = integrationTypeRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualIntegrationType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var typeIdToDelete = AddandGetTestIntegrationType().ID;
            IntegrationType actualIntegrationType;

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                integrationTypeRepo.Delete(typeIdToDelete);
                integrationTypeRepo.SaveChanges();
                actualIntegrationType = integrationTypeRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualIntegrationType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllIntegrationTypesReturned_CountEqualsActual()
        {
            const int expectedCount = 1;
            int actualCount;
            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                actualCount = integrationTypeRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedIntegrationType = new IntegrationType
            {
                ID = 1,
                Description = "Twitch"
            };
            IntegrationType actualIntegrationType;

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                actualIntegrationType = integrationTypeRepo.GetById(expectedIntegrationType.ID);
            }

            AssertIntegrationTypesEqual(expectedIntegrationType, actualIntegrationType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_ActualUpdatedData_ActualEqualsExpectedData()
        {
            var expectedIntegrationType = new IntegrationType
            {
                ID = 1,
                Description = "Beam"
            };
            IntegrationType actualIntegrationType;

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                integrationTypeRepo.Update(expectedIntegrationType);
                integrationTypeRepo.SaveChanges();
                actualIntegrationType = integrationTypeRepo.GetById(expectedIntegrationType.ID);
            }

            AssertIntegrationTypesEqual(expectedIntegrationType, actualIntegrationType);
        }

        private IntegrationType AddandGetTestIntegrationType()
        {
            var integrationType = new IntegrationType
            {
                ID = 2,
                Description = "TestType"
            };

            using (var integrationTypeRepo = new IntegrationTypeRepository(new brothershipEntities()))
            {
                integrationTypeRepo.Add(integrationType);
                integrationTypeRepo.SaveChanges();
            }

            return integrationType;
        }

        private void AssertIntegrationTypesEqual(IntegrationType expected, IntegrationType actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Description, actual.Description);
        }
    }
}
