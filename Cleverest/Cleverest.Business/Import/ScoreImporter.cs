using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Business.InterfaceDefinitions.Managers.ExcelManagers;

namespace Cleverest.Business.Import
{
    public class ScoreImporter
    {
        protected string ImportFolder { get; set; }

        protected IGameManager GameManager;

        protected IExcelScoreManager ExcelScoreManager;

        protected IScoreManager ScoreManager;

        protected ScoreValidator Validator;

        public ScoreImporter(IGameManager manager, IExcelScoreManager excelManager, IScoreManager scoreManager)
        {
            this.GameManager = manager;
            this.ExcelScoreManager = excelManager;
            this.ScoreManager = scoreManager;

            this.Validator = new ScoreValidator();
        }

        protected virtual void ConfigureValidationRules()
        {
            Validator.AddRule(score =>
            {
                return !score.GameId.IsEmpty() && !score.TeamId.IsEmpty()
                        && score.Value >= 0 && score.RoundNo > 0;
            });
        }

        public void Import(string importFolderPath)
        {
            SetImportFolder(importFolderPath);
            ConfigureValidationRules();

            try
            { 
                foreach (var file in new DirectoryInfo(ImportFolder).GetFiles("*.xlsx"))
                {
                    ProcessFile(file);
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected bool ProcessFile(FileInfo file)
        {
            var gameId = ExtractGameId(file);
            if (!EnsureGameExist(gameId))
                return false;

            var scores = ExcelScoreManager.GetScores(file);
            foreach(var score in scores)
            {
                score.GameId = gameId;
                if (Validator.IsScoreValid(score))
                    ScoreManager.Create(score);
            }

            return true;
        }

        protected string ExtractGameId(FileInfo scoreFile)
        {
            return Path.GetFileNameWithoutExtension(scoreFile.FullName);
        }

        protected bool EnsureGameExist(string gameId)
        {
            if (string.IsNullOrEmpty(gameId))
                return false;

            return GameManager.Get(gameId) != null;
        }

        protected string GetConnectionString(string path)
        {
            var props = new Dictionary<string, string>();

            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = path;

            var sb = new StringBuilder();
            foreach(var prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(";");
            }

            return sb.ToString();
        }

        protected void SetImportFolder(string path)
        {
            if (Path.IsPathRooted(path))
                this.ImportFolder = path;
            else
                this.ImportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            EnsureImportFolder();
        }

        protected virtual void EnsureImportFolder()
        {
            if (!Directory.Exists(ImportFolder))
                Directory.CreateDirectory(ImportFolder);
        }
    }
}
