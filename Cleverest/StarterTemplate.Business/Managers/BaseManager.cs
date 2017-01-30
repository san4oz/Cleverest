using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.InterfaceDefinitions.Providers;
using System.Web.Http.Dependencies;
using System.Web.Mvc;

namespace Cleverest.Business.Managers
{
    public abstract class BaseManager<T, TProvider> : IBaseManager<T>
        where TProvider : IBaseProvider<T>
    {
        public virtual TProvider Provider
        {
            get { return DependencyResolver.Current.GetService<TProvider>(); }
        }

        public virtual T Get(string id)
        {
            return Provider.Get(id);
        }

        public virtual IList<T> All()
        {
            return Provider.All();
        }

        public virtual void Create(T entity)
        {
            Provider.Create(entity);
        }

        public virtual void Delete(string id)
        {
            Provider.Delete(id);
        }
    }
}
