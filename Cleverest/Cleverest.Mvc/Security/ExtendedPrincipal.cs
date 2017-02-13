using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Cleverest.Mvc.Security
{
    public class ExtendedPrincipal : IExtendedPrincipal
    {
        public IIdentity Identity { get; private set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public ExtendedPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public bool IsInRole(string role)
        {
            return Identity != null
                && Identity.IsAuthenticated
                && !string.IsNullOrEmpty(role)
                && Roles.IsUserInRole(role);
        }

        public bool IsAdmin
        {
            get
            {
                return IsInRole("Admin");
            }
        }
    }
}
