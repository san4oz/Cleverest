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
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string SocialNetworkLink { get; set; }
    }
}
