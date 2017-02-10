using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class AccountViewModel : BaseViewModel, IMapFrom<Account>
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Команда")]
        public string TeamId { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        public List<SelectListItem> TeamsIds { get; set; }

        public AccountViewModel()
        {
            TeamsIds = new List<SelectListItem>();
        }
    }
}
