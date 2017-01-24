using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace Cleverest.DataProvider
{
    public class NhibernateSessionHelper
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration().Configure();
            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
