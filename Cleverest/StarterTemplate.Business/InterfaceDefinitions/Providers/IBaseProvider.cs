using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.InterfaceDefinitions.Providers
{
    public interface IBaseProvider<T>
    {
        bool Create(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        T Get(string id);

        IList<T> All();
    }
}
