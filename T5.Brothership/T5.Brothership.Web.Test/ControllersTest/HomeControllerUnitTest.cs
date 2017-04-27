using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.Controllers;
using T5.Brothership.BL.Test.ManagerFakes;
using System.Web.Mvc;

namespace T5.Brothership.Web.Test.ControllersTest
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void Index_CorrectViewReturned_ExpectedViewNameEqualsActual()
        {
            using (HomeController controller = new HomeController(new UserManagerFake()))
            {
                var result = controller.Index() as ViewResult;
                var expectedViewName = "Index";

                Assert.AreEqual(expectedViewName, result.ViewName);
            }
        }
    }
}
