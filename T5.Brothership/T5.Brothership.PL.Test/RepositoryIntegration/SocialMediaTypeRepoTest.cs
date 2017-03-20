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
    public class SocialMediaTypeRepoTest
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
        public void Add_WasSocialMediaTypeAdded_ActualEqualsExpectedData()
        {
            var expectedSocialMediaType = new SocialMediaType
            {
                Description = "NewType"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                socialMediaTypeRepo.Add(expectedSocialMediaType);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(expectedSocialMediaType.ID);
            }

            AssertSocialMediaTypesEqual(expectedSocialMediaType, actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_ActualDataIdNull()
        {
            SocialMediaType actualSocialMediaType;
            var typeToDelete = AddandGetTestSocialMediaType();

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                socialMediaTypeRepo.Delete(typeToDelete);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_ActualDataIsNull()
        {
            var typeIdToDelete = AddandGetTestSocialMediaType().ID;
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                socialMediaTypeRepo.Delete(typeIdToDelete);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_AllMediaTypesReturned_CountEqualsActual()
        {
            const int expectedCount = 3;
            int actualCount;
            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualCount = socialMediaTypeRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_ActualEqualsExpectedData()
        {
            var expectedSocialMediaType = new SocialMediaType
            {
                ID = 1,
                Description = "Youtube"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                actualSocialMediaType = socialMediaTypeRepo.GetById(expectedSocialMediaType.ID);
            }

            AssertSocialMediaTypesEqual(expectedSocialMediaType, actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_WasSocialMediaTypeUpdated_ActualEqualsExpectedData()
        {
            var expectedSocialMediaType = new SocialMediaType
            {
                ID = 1,
                Description = "NewType"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                socialMediaTypeRepo.Update(expectedSocialMediaType);
                actualSocialMediaType = socialMediaTypeRepo.GetById(expectedSocialMediaType.ID);
            }

            AssertSocialMediaTypesEqual(expectedSocialMediaType, actualSocialMediaType);
        }

        private SocialMediaType AddandGetTestSocialMediaType()
        {
            var socialMediaTypeType = new SocialMediaType
            {
                Description = "TestType"
            };

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository( new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME)))
            {
                socialMediaTypeRepo.Add(socialMediaTypeType);
                socialMediaTypeRepo.SaveChanges();
            }

            return socialMediaTypeType;
        }

        private void AssertSocialMediaTypesEqual(SocialMediaType expected, SocialMediaType actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Description, actual.Description);
        }
    }
}
