using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Helpers;

namespace T5.Brothership.BL.Test.HelpersUnitTest
{
    [TestClass]
    public class TwitterURLConverterUnitTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void GetUserName_CorrectUserNameReturn_ActualEqualsExpected()
        {
            const string url = "https://twitter.com/BrotherShipDev";
            const string expectedUserName = "BrotherShipDev";

            TwitterURLConverter converter = new TwitterURLConverter();
            string actualUserName = converter.GetUserName(url);

            Assert.AreEqual(expectedUserName, actualUserName);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void GetURL_CorrectURLReturned_ActualEqualsExpected()
        {
            const string userName = "BrotherShipDev";
            const string expectedURL = "https://twitter.com/BrotherShipDev";

            TwitterURLConverter converter = new TwitterURLConverter();
            string actualURL = converter.GetURL(userName);

            Assert.AreEqual(expectedURL, actualURL);
        }
    }
}
