using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T5.Brothership.Helpers
{
    public class SessionHelper : ISessionHelper
    {
        public void Add(string sessionKey, object sessionValue)
        {
            HttpContext.Current.Session.Add(sessionKey, sessionValue);
        }

        public object Get(string sessionKey)
        {
            return HttpContext.Current.Session[sessionKey];
        }

        public void remove(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }
    }
}