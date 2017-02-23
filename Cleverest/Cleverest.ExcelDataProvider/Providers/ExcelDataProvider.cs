using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.ExcelDataProvider.Providers
{
    public class ExcelDataProvider<T>
    {
        private bool IsConfigured;

        protected string DataSource { get; set; }

        public void SetDataSource(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                return;

            this.DataSource = sourcePath;
            this.IsConfigured = true;
        }

        protected void Execute(Action<OleDbConnection> action)
        {
            if (!this.IsConfigured)
                throw new ArgumentException("Source file is missing");

            using (var connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                action(connection);
            }
        }

        protected E Execute<E>(Func<OleDbConnection, E> function)
        {
            if (!this.IsConfigured)
                throw new ArgumentException("Source file is missing");

            using (var connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                return function(connection);
            }
        }

        protected string GetConnectionString()
        {
            var props = new Dictionary<string, string>();

            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = DataSource;

            var sb = new StringBuilder();
            foreach (var prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(";");
            }

            return sb.ToString();
        }
    }
}
