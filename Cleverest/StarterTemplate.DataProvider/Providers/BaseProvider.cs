using Cleverest.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Cleverest.DataProvider;
using Cleverest.Business.Entities;

namespace Cleverest.DataProvider.Providers
{
    public class BaseProvider<T> : IBaseProvider<T>
        where T : Entity
    {
        public bool Create(T entity)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            });

            return true;
        }

        public bool Delete(string id)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    var valueToBeRemoved = session.Get<T>(id);
                    if (valueToBeRemoved != null)
                    {
                        session.Delete(valueToBeRemoved);
                        transaction.Commit();
                    }
                }
            });

            return true;
        }

        public T Get(string id)
        {
            return Execute<T>(session =>
            {
                return session.Get<T>(id);
            });
        }

        public IList<T> All()
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<T>();

                return criteria.List<T>();
            });
        }

        #region helpers
        protected T Execute<T>(Func<ISession, T> expression)
        {
            using (var session = NhibernateSessionHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected bool Execute(Func<ISession, bool> expression)
        {
            using (var session = NhibernateSessionHelper.OpenSession())
            {
                return expression(session);
            }
        }

        protected void Execute(Action<ISession> expression)
        {
            using (var session = NhibernateSessionHelper.OpenSession())
            {
                expression(session);
            }
        }


        #endregion
    }
}
