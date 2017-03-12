using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T5.Brothership.PL.Test
{
    public static class SqlScriptRunner
    {
        public static void RunAddTestDataScript(brothershipEntities dbContext)
        {
            var script = File.ReadAllText(@"../../../T5.Brothership.PL.Test/AddTestData.sql");
            dbContext.Database.ExecuteSqlCommand(script);
        }
    }
}
