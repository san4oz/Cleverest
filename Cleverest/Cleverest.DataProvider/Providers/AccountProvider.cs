using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Providers;

namespace Cleverest.DataProvider.Providers
{
    public class AccountProvider: BaseProvider<Account>, IAccountProvider
    {
    }
}
