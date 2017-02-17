using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels
{
    public class TeamViewModel : BaseViewModel, IMapFrom<Team>
    {
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Кількість гравців")]
        public int ParticipantsCount { get; set; }
    }
}
