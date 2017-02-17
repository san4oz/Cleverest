using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.Entities.Search;

namespace Cleverest.Business.InterfaceDefinitions.Search
{
    public interface IBaseSearchManager<T>
        where T: Entity
    {
        void AddToIndex(T entity);

        void AddToIndex(IList<T> entities);

        SearchResponse Search(SearchRequest request);
    }
}
