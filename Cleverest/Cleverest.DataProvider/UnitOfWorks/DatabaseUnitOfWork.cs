using Cleverest.Business.InterfaceDefinitions.UnitOfWorks;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.DataProvider.UnitOfWorks
{
    public sealed class DatabaseUnitOfWork : IDatabaseUnitOfWork, IDisposable
    {
        ISession session = null;

        public DatabaseUnitOfWork()
        {
            session = NhibernateSessionHelper.OpenSession();
        }

        public void Execute(Action action, string errorMessage = null)
        {
            Execute<Object>(() => { action(); return null; }, errorMessage);
        }

        public TResult Execute<TResult>(Func<TResult> func, string errorMessage = null)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                //Log ex
                session.Transaction.Rollback();
                throw;
            }
        }

        public void Commit()
        {
            session.Transaction.Commit();
            session.Dispose();
        }

        public void Rollback()
        {
            session.Transaction.Rollback();
            session.Dispose();
        }

        public void Flush()
        {
            session.Flush();
        }

        public void Dispose()
        {
            if (session != null)
            {
                session.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
