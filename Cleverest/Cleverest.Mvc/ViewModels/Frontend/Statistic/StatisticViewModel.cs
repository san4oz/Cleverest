using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Frontend.Statistic
{
    public class StatisticViewModel
    {
        public GeneralCountersModel GeneralCounters { get; set; }

        public BestResultsModel BestResults { get; set; }
    }
}
