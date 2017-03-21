using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    /// <summary>
    /// Summary description for GenderManagerUnitTest
    /// </summary>
    [TestClass]
    public class GenderManagerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetAll_AllGendersReturned_ExpectedCountEqualsActualCount()
        {
            const int expectedCount = 3;
            using (var genderManager = new GenderManager(new FakeBrothershipUnitOfWork()))
            {
                var actualCount = genderManager.GetAll().Count;
                Assert.AreEqual(expectedCount, actualCount);
            }
        }
    }
}
