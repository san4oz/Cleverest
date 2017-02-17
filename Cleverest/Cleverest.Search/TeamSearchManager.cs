using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.Entities.Search;
using Cleverest.Business.InterfaceDefinitions.Search;
using Lucene.Net.Documents;

namespace Cleverest.Search
{
    public class TeamSearchManager : BaseSearchManager<Team>, ITeamSearchManager
    {
        protected override IndexInfo GetIndexInfo()
        {
            var indexInfo = new IndexInfo();
            indexInfo.KeywordFields = new List<string>()
            {
                IndexConstants.TeamIndexFields.Name
            };

            return indexInfo;
        }
    }
}
