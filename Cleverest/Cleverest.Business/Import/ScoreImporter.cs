using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.InterfaceDefinitions.Managers;

namespace Cleverest.Business.Import
{
    public class ScoreImporter
    {
        protected string ImportFolder { get; set; }

        protected IGameManager GameManager;

        public ScoreImporter(IGameManager manager)
        {
            this.GameManager = manager;
        }

        public void Import(string importFolderPath)
        {
            if (!PrepareImportFolder(importFolderPath))
                return;

            foreach (var file in new DirectoryInfo(ImportFolder).GetFiles("*.xlsx"))
            {
                ProcessFile(file);
            }
        }

        protected bool ProcessFile(FileInfo file)
        {
            var gameId = ExtractGameId(file);
            if (!EnsureGameExist(gameId))
                return false;

            var dataSet = ExtractDataSetFromFile(file);

            return true;
        }

        protected DataSet ExtractDataSetFromFile(FileInfo file)
        {
            DataSet result = new DataSet();

            var connectionString = GetConnectionString(file.FullName);
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var command = new OleDbCommand();
                command.Connection = connection;

                var dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dataRow in dtSheet.Rows)
                {
                    string sheetName = dataRow["TABLE_NAME"].ToString();
                    if (!sheetName.EndsWith("$"))
                        continue;

                    command.CommandText = "SELECT * FROM [" + sheetName + "]";

                    var dataTable = new DataTable();
                    dataTable.TableName = sheetName;

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);

                    result.Tables.Add(dataTable);
                }
            }

            return result;
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

        protected virtual bool PrepareImportFolder(string relativePath)
        {
            try
            { 
                var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                this.ImportFolder = fullPath;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
