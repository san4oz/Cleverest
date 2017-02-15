using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Search;
using Lucene.Net.Documents;

namespace Cleverest.Search
{
    public class TeamSearchManager : BaseSearchManager<Team>, ITeamSearchManager
    {
        public override List<string> SearchableFields
        {
            get { return new List<string>() { IndexConstants.Team.Name }; }
        }

        public override Document CreateDocument(Team entity)
        {
            var document = new Document();

            document.Add(new Field(IndexConstants.Id, entity.Id, Field.Store.YES, Field.Index.NO));
            document.Add(new Field(IndexConstants.Team.Name, entity.Name, Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        public override Team GetData(Document doc)
        {
            var team = new Team();

            team.Id = doc.Get(IndexConstants.Id);
            team.Name = doc.Get(IndexConstants.Team.Name);

            return team;
        }
    }
}
