using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Helpers
{
    public class SessionManager
    {
        private readonly HttpSessionState _session;
        private string _sessionKey = "isLoggedIn";

        public SessionManager()
        {
            _session = HttpContext.Current.Session;
        }

         public SessionManager(string sessionKey) : this()
        {
            _sessionKey = sessionKey;
        }

        //set the login status in session for the current user.
        public void setIsLoggedIn(string value)
        {
            _session[_sessionKey] = value;
        }

        public void removeSession()
        {
            _session[_sessionKey] = null;
        }

        public bool IsNull()
        {
            return _session[_sessionKey] == null;
        }
        
         //check to see if the user is logged in
        public bool isLoggedIn()
        {
            if (_session[_sessionKey].Equals(null))
                return false;
            else
                return true;
        }

        public void Remove()
        {
            _session.Remove(_sessionKey);
        }

        public string GetSessionId()
        {
            return _session.SessionID;
        }

        public bool HasAnySessions()
        {
            return _session.Count > 0;
        }

        public void RemoveAll()
        {
            _session.RemoveAll();
        }

        public void AbandonSessions()
        {
            _session.Abandon();
        }

    }
}
