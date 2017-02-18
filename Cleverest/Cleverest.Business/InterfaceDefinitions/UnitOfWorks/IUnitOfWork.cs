using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.InterfaceDefinitions.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();

        void Flush();
    }
}
