using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;

namespace Cleverest.Business.InterfaceDefinitions.Search
{
    public interface IBaseSearchManager<T>
        where T: Entity
    {
        void AddToIndex(T entity);

        void AddToIndex(IList<T> entities);

        List<T> Search(string query);
    }
}
