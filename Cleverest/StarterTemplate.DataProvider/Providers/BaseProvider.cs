using Cleverest.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.DataProvider.Providers
{
    public class BaseProvider<T> : IBaseProvider<T>
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
