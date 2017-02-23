using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Import;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Managers.ExcelManagers;

namespace Cleverest.Business.Tasks
{
    public class ScoreImportTask : ITask
    {
        public ScoreImporter Importer;

        public string Name { get { return "ScoreImport"; } }

        public ScoreImportTask(IGameManager manager, IExcelScoreManager excelScoreManager, IScoreManager scoreManager)
        {
            this.Importer = new ScoreImporter(manager, excelScoreManager, scoreManager); 
        }

        public void Run()
        {
            Importer.Import(@"Content\Files\Import\ScoreImport\");
        }
    }
}
