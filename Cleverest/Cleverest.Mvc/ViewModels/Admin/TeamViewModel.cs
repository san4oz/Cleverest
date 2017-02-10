using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class TeamViewModel : BaseViewModel, IMapFrom<Team>
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Посилання на соц. мережу")]
        public string SocialNetworkLink { get; set; }
    }
}
