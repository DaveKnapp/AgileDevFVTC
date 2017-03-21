using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Managers;
using T5.Brothership.PL.Test;

namespace T5.Brothership.BL.Test.ManagerUnitTests
{
    [TestClass]
    public class NationalitymanagerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetAll_AllNationaliesReturned_ExpectedCountEqualsActual()
        {
            const int expectedCount = 5;
            using (var nationalityManager = new NationalityManager(new FakeBrothershipUnitOfWork()))
            {
                var actualCount = nationalityManager.GetAll().Count;
                Assert.AreEqual(expectedCount, actualCount);
            }
        }
    }
}
