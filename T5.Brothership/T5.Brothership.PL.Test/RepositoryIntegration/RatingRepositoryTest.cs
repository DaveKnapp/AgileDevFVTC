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
    public class RatingRepositoryTest
    {
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
        public void Add_ActualAddedData_EqualsExpectedData()
        {
            var expectedRating = new Rating
            {
                Description = "ZeroStars"
            };
            Rating actualRating;

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                ratingRepo.Add(expectedRating);
                ratingRepo.SaveChanges();
                actualRating = ratingRepo.GetById(expectedRating.ID);
            }

            AssertRatingsEqual(expectedRating, actualRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteByEntity_WasDeleted_actualDataNull()
        {
            Rating actualRating;
            var typeToDelete = AddandGetTestRating();

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                ratingRepo.Delete(typeToDelete);
                ratingRepo.SaveChanges();
                actualRating = ratingRepo.GetById(typeToDelete.ID);
            }

            Assert.IsNull(actualRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void DeleteById_WasDeleted_actualDataNull()
        {
            var typeIdToDelete = AddandGetTestRating().ID;
            Rating actualRating;

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                ratingRepo.Delete(typeIdToDelete);
                ratingRepo.SaveChanges();
                actualRating = ratingRepo.GetById(typeIdToDelete);
            }

            Assert.IsNull(actualRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetAll_Count_EqualActual()
        {
            const int expectedCount = 5;
            int actualCount;
            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                actualCount = ratingRepo.GetAll().Count();
            }

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void GetById_CorrectDataGot_EqualExpectedData()
        {
            var expectedRating = new Rating
            {
                ID = 1,
                Description = "One Star"
            };
            Rating actualRating;

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                actualRating = ratingRepo.GetById(expectedRating.ID);
            }

            AssertRatingsEqual(expectedRating, actualRating);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_ActualUpdatedData_EqualsExpectedData()
        {
            var expectedRating = new Rating
            {
                ID = 1,
                Description = "Beam"
            };
            Rating actualRating;

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                ratingRepo.Update(expectedRating);
                actualRating = ratingRepo.GetById(expectedRating.ID);
            }

            AssertRatingsEqual(expectedRating, actualRating);
        }

        private Rating AddandGetTestRating()
        {
            var ratingType = new Rating
            {
                Description = "TestRating"
            };

            using (var ratingRepo = new RatingRepository(new brothershipEntities()))
            {
                ratingRepo.Add(ratingType);
                ratingRepo.SaveChanges();
            }

            return ratingType;
        }

        private void AssertRatingsEqual(Rating expected, Rating actual)
        {
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Description, actual.Description);
        }

    }
}
