using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.Security
{
    public interface IExtendedPrincipal : IPrincipal
    {
        string Id { get; set; }

        string Email { get; set; }

        string Name { get; set; }

        bool IsAdmin { get; }
    }
}
