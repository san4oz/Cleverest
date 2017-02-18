using Cleverest.Business.InterfaceDefinitions.Providers;
using Cleverest.Business.InterfaceDefinitions.UnitOfWorks;
using Cleverest.DataProvider.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.DataProvider.Providers
{
    public class DatabaseUnitOfWorkProvider : IDatabaseUnitOfWorkProvider
    {
        public IDatabaseUnitOfWork CreateUnitOfWork()
        {
            return new DatabaseUnitOfWork();
        }
    }
}
