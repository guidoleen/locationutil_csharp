using System;

namespace LocationUtil
{
    public class SessionLocation
    {
        private int klantId;
        private String sessionId;
        private String sessionToken;

        public SessionLocation(int _klantId, String _sessionId, String _sessionToken)
        {
            this.klantId = _klantId;
            this.sessionId = _sessionId;
            this.sessionToken = _sessionToken;
        }

        public int getKlantId()
        {
            return this.klantId;
        }

        public String getSessionId()
        {
            return this.sessionId;
        }

        public String getSessionToken()
        {
            return this.sessionToken;
        }
    }
}

