using Cleverest.Business.InterfaceDefinitions.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.InterfaceDefinitions.Providers
{
    public interface IDatabaseUnitOfWorkProvider
    {
        IDatabaseUnitOfWork CreateUnitOfWork();
    }
}
