using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;
using System.Linq;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class RatingManagerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetAll_AllRatingsReturned_ExpectedCountEqualAcual()
        {
            using (RatingManager ratingManger = new RatingManager(new FakeBrothershipUnitOfWork()))
            {
                const int expectedCount = 5;
                int actualCount = ratingManger.GetAll().Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
        }
    }
}
