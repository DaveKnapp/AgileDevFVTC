using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace T5.Brothership.PL.Test
{
    /// <summary>
    /// Summary description for DatabaseIntegrationTest
    /// </summary>
    [TestClass]
    public class DatabaseIntegrationTest
    {
        [TestMethod, TestCategory("IntegrationTest")]
        public void IsDatabaseConnected_DatabaseConnected_StateEqualsOpen()
        {
            using (brothershipEntities dbContext = new brothershipEntities())
            {
                dbContext.Database.Connection.Open();
                Assert.IsTrue(dbContext.Database.Connection.State == System.Data.ConnectionState.Open);
            }
        }
    }
}
