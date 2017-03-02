using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T5.Brothership.BL.Helpers;

namespace T5.Brothership.BL.Test.HelpersUnitTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PasswordHelperTest
    {
        [TestMethod, TestCategory("UnitTest")]
        public void IsPasswordMatch_MatchingPasswords_Returns()
        {
            string expectedPassword = "Password";

            PasswordHelper passwordManager = new PasswordHelper();

            HashedPassword actualHashedPassword = passwordManager.GeneratePasswordHash(expectedPassword);

            Assert.IsTrue(passwordManager.IsPasswordMatch(expectedPassword, actualHashedPassword));
        }

        [TestMethod, TestCategory("UnitTest")]
        public void IsPasswordMatch_UnmatchingPasswords_ReturnsFalse()
        {
            string expectedPassword = "Password";

            PasswordHelper passwordManager = new PasswordHelper();

            HashedPassword actualHashedPassword = new HashedPassword();
            actualHashedPassword = passwordManager.GeneratePasswordHash(expectedPassword);

            expectedPassword = "Mr. T";
            Assert.IsFalse(passwordManager.IsPasswordMatch(expectedPassword, actualHashedPassword));
        }

    }
}
