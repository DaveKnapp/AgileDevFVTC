using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;

namespace T5.Brothership.PL.Test
{
    public static class DataContextCreator
    {
        public static brothershipEntities CreateTestContext()
        {
            return new brothershipEntities(ConnectionStrings.TEST_CONNECTION_STRING_NAME);
        }
    }
}
