using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class TeamDetailsInfoViewModel : BaseViewModel, IMapFrom<Team>
    {
        public string Name { get; set; }

        public string PhotoPath { get; set; }

        public string Description { get; set; }
    }
}
