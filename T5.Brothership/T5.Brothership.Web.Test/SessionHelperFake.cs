using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.Helpers;

namespace T5.Brothership.Web.Test
{
    public class SessionHelperFake : ISessionHelper
    {
        private Dictionary<string, object> _fakeSession = new Dictionary<string, object>();

        public void Add(string sessionKey, object sessionValue)
        {
            _fakeSession.Add(sessionKey, sessionValue);
        }

        public object Get(string sessionKey)
        {
            return _fakeSession[sessionKey];
        }
    }
}
