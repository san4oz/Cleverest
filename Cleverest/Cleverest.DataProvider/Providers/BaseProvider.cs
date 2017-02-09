using Cleverest.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Cleverest.DataProvider;
using Cleverest.Business.Entities;
using NHibernate.Criterion;

namespace Cleverest.DataProvider.Providers
{
    public class BaseProvider<T> : IBaseProvider<T>
        where T : Entity
    {

        public virtual List<T> GetByIdList(List<string> ids)
        {
            return Execute(session =>
            {
                var criteria = session.CreateCriteria<T>();

                criteria.Add(Expression.In("Id", ids.ToArray()));

                return criteria.List<T>().ToList();
            });
        }

        public virtual bool Create(T entity)
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

        public virtual bool Update(string id, Action<T> updateAction)
        {
            Execute(session =>
            {
                using (var transaction = session.BeginTransaction())
                {
                    var entityToUpdate = session.Get<T>(id);

                    updateAction(entityToUpdate);
                    
                    session.Update(entityToUpdate);

                    transaction.Commit();
                }
            });

            return true;
        }


        public virtual bool Delete(string id)
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

        public virtual T Get(string id)
        {
            return Execute<T>(session =>
            {
                return session.Get<T>(id);
            });
        }

        public virtual IList<T> All()
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
