using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.ExcelDataProvider.Entities
{
    public class ExcelScoreRow
    {
        public string TeamName { get; set; }

        public Dictionary<int, int> Scores { get; set; }
    }
}
