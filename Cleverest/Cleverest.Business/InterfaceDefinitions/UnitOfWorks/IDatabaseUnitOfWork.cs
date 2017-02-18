using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.InterfaceDefinitions.UnitOfWorks
{
    public interface IDatabaseUnitOfWork : IUnitOfWork
    {
        void Execute(Action action, string errorMessage = null);

        TResult Execute<TResult>(Func<TResult> func, string errorMessage = null);
    }
}
