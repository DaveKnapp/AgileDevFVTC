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
    /// <summary>
    /// Summary description for SocialMediaTypeRepoTest
    /// </summary>
    [TestClass]
    public class SocialMediaTypeRepoTest
    {

        [TestMethod, TestCategory("IntegrationTest")]
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            var expectedSocialMediaType = new SocialMediaType
            {
                Description = "NewType"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
            {
                socialMediaTypeRepo.Add(expectedSocialMediaType);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(expectedSocialMediaType.ID);
            }

            AssertSocialMediaTypesEqual(expectedSocialMediaType, actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_actualDataNull()
        {
            SocialMediaType actualSocialMediaType;
            var typeToDelete = AddandGetTestSocialMediaType();

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
            {
                socialMediaTypeRepo.Delete(typeToDelete);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_actualDataNull()
        {
            var typeIdToDelete = AddandGetTestSocialMediaType().ID;
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
            {
                socialMediaTypeRepo.Delete(typeIdToDelete);
                socialMediaTypeRepo.SaveChanges();
                actualSocialMediaType = socialMediaTypeRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualSocialMediaType);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualActual()
        {
            const int expectedCount = 3;
            int actualCount;
            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
            {
                actualCount = socialMediaTypeRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
        {
            var expectedSocialMediaType = new SocialMediaType
            {
                ID = 1,
                Description = "Youtube"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
            {
                actualSocialMediaType = socialMediaTypeRepo.GetById(expectedSocialMediaType.ID);
            }

            AssertSocialMediaTypesEqual(expectedSocialMediaType, actualSocialMediaType);
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
            var expectedSocialMediaType = new SocialMediaType
            {
                ID = 1,
                Description = "NewType"
            };
            SocialMediaType actualSocialMediaType;

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
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

            using (var socialMediaTypeRepo = new SocialMediaTypeRepository(new brothershipEntities()))
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
