using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Mvc.ViewModels.Frontend.Statistic
{
    public class BestResultsModel
    {
        public TeamInfo TheMostSuccessful { get; set; }

        public TeamInfo TheMostRegularVisitors { get; set; }

        public TeamInfo TheFunniestOne { get; set; }

        public class TeamInfo
        {
            public string ImageSrc { get; set; }

            public string Name { get; set; }

            public decimal Score { get; set; }
        }
    }
}
