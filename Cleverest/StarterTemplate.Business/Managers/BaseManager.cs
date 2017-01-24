using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Managers
{
    public abstract class BaseManager<T> : IBaseManager<T>
    {
        public bool Create(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(string id)
        {
            throw new NotImplementedException();
        }

        public IList<T> All()
        {
            throw new NotImplementedException();
        }
    }
}
