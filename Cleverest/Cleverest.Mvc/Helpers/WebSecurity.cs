using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cleverest.Mvc.Security;

namespace Cleverest.Mvc.Helpers
{
    public static class WebSecurity
    {
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public static bool IsUserContextAvailable()
        {
            return Context != null &&
                    Context.User != null &&
                    Context.User.Identity != null &&
                    Context.User.Identity.IsAuthenticated;              
        }

        public static ExtendedPrincipal User
        {
            get
            {
                return IsUserContextAvailable() ? (ExtendedPrincipal)Context.User : null;
            }
        }
    }
}
