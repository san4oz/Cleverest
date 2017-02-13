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
    public class ProfileViewModel : BaseViewModel, IMapFrom<Account>
    {
        [Display(Name = "Електронна адреса")]
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Сторінка у соц. мережі")]
        public string SocialNetworkLink { get; set; }
    }
}
