using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Managers.ExcelManagers;
using Cleverest.ExcelDataProvider.Entities;
using Cleverest.ExcelDataProvider.Providers;

namespace Cleverest.ExcelDataProvider.Managers
{
    public class ExcelScoreManager : ExcelManagerBase<Score>, IExcelScoreManager
    {
        protected ITeamManager TeamManager;

        protected IGameManager GameManager;

        protected ExcelScoreDataProvider Provider;

        public ExcelScoreManager(ITeamManager teamManager, IGameManager gameManager)
        {
            this.TeamManager = teamManager;
            this.GameManager = gameManager;

            this.Provider = new ExcelScoreDataProvider();
        }

        public IList<Score> GetScores(FileInfo file)
        {
            Provider.SetDataSource(file.FullName);

            return Provider.GetScores().SelectMany(row => ParseScoreRow(row)).ToList();
        }

        public IList<Score> ParseScoreRow(ExcelScoreRow row)
        {
            var result = new List<Score>();

            var team = TeamManager.GetTeamByName(row.TeamName);
            if (team == null)
                return null;

            foreach(var hint in row.Scores)
            {
                result.Add(new Score()
                {
                    TeamId = team.Id,
                    RoundNo = hint.Key,
                    Value = hint.Value
                });                
            }

            return result;            
        }
    }
}
