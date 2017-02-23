using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.Business.Entities;
using Cleverest.ExcelDataProvider.Entities;

namespace Cleverest.ExcelDataProvider.Providers
{
    public class ExcelScoreDataProvider : ExcelDataProvider<Score>
    {
        public const int DefaultRoundsCount = 7;

        public IList<ExcelScoreRow> GetScores()
        {
            var dataSet = GetData();

            return ParseScores(dataSet);
        }

        protected IList<ExcelScoreRow> ParseScores(DataSet set)
        {
            var result = new List<ExcelScoreRow>();

            foreach (DataRow row in set.Tables[0].Rows)
            {
                try
                {
                    result.Add(new ExcelScoreRow()
                    {
                        TeamName = ExtractTeamName(row),
                        Scores = ExtractTeamScores(row)
                    });
                }
                catch (Exception ex) { break; }
            }

            return result;
        }

        protected string ExtractTeamName(DataRow row)
        {
            return (string)row[(int)ExcelColumnsDescription.Name];
        }

        protected Dictionary<int, int> ExtractTeamScores(DataRow row)
        {
            var result = new Dictionary<int, int>();
            
            for(int i = 1; i <= DefaultRoundsCount; i++)
            {
                result.Add(i, GetScoreValue(row, i));
            }

            return result;
        }

        protected int GetScoreValue(DataRow row, int round)
        {
            var roundIndex = GetActualRoundIndex(round);
            if (roundIndex < 0)
                throw new IndexOutOfRangeException("Incorrect index");

            return Convert.ToInt32(row[roundIndex]);
        }

        protected virtual int GetActualRoundIndex(int round)
        {
            switch (round)
            {
                case 1:
                    return (int)ExcelColumnsDescription.FirstRound;
                case 2:
                    return (int)ExcelColumnsDescription.SecondRound;
                case 3:
                    return (int)ExcelColumnsDescription.ThirdRound;
                case 4:
                    return (int)ExcelColumnsDescription.FourthRound;
                case 5:
                    return (int)ExcelColumnsDescription.FifthRound;
                case 6:
                    return (int)ExcelColumnsDescription.SixthRound;
                case 7:
                    return (int)ExcelColumnsDescription.SeventhRound;
                default:
                    return -1;
            }
        }

        protected DataSet GetData()
        {
            DataSet result = new DataSet();

            Execute(connection =>
            {
                foreach (DataRow dataRow in connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows)
                {
                    string sheetName = dataRow["TABLE_NAME"].ToString();
                    if (!sheetName.EndsWith("$"))
                        continue;

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("Select * from [{0}]", sheetName);
                        var adapter = new OleDbDataAdapter(command);

                        var dataTable = new DataTable(sheetName);
                        adapter.Fill(dataTable);

                        result.Tables.Add(dataTable);
                    }
                }
            });

            return result;
        }
    }

    public enum ExcelColumnsDescription : int
    {
        Name = 1,
        FirstRound = 2,
        SecondRound = 3,
        ThirdRound = 4,
        FourthRound = 6,
        FifthRound = 7,
        SixthRound = 8,
        SeventhRound = 9
    }
}
