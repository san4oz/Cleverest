using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Cleverest.DataProvider
{
    public class NhibernateSessionHelper
    {
        private static readonly ISessionFactory sessionFactory;

        static NhibernateSessionHelper()
        {
            var configuration = new Configuration().Configure();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                return sessionFactory.GetCurrentSession();
            }

            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public static void Unbind()
        {
            CurrentSessionContext.Unbind(sessionFactory);
        }

        //public static ISession OpenSession()
        //{
        //    var configuration = new Configuration().Configure();
        //    var sessionFactory = configuration.BuildSessionFactory();
        //    return sessionFactory.OpenSession();
        //}
    }
}
