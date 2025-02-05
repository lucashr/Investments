using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Investments.VariablesManager
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SessionContext _sessionContext;

        public SessionService(IHttpContextAccessor httpContextAccessor, SessionContext sessionContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _sessionContext = sessionContext;
        }

        public void LoadSessionId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                _sessionContext.SessionId = context.Request.Query["sessionId"];
            }
        }
    }

}