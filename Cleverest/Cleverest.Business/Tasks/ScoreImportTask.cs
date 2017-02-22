using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Import;
using Cleverest.Business.InterfaceDefinitions.Managers;

namespace Cleverest.Business.Tasks
{
    public class ScoreImportTask : ITask
    {
        public ScoreImporter Importer;

        public string Name { get { return "ScoreImport"; } }

        public ScoreImportTask(IGameManager manager)
        {
            this.Importer = new ScoreImporter(manager); 
        }

        public void Run()
        {
            Importer.Import(@"Content\Files\Import\ScoreImport\");
        }
    }
}
