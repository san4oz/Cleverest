using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.InterfaceDefinitions.Managers
{
    public interface IBaseManager<T>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(string id);

        T Get(string id);

        IList<T> All();

        IList<T> GetByIdList(List<string> ids);
    }
}
