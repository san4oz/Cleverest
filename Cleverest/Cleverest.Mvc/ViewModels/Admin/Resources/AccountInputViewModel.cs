using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cleverest.App_Start.AutoMapper;
using Cleverest.Business.Entities;

namespace Cleverest.Mvc.ViewModels.Admin.Resources
{
    public class AccountInputViewModel : BaseInputViewModel, IMapFrom<Account>
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

        [Display(Name = "Фото")]
        public HttpPostedFileBase Image { get; set; }

        public List<SelectListItem> AllTeamsSelectList { get; set; }

        public AccountInputViewModel()
        {
            AllTeamsSelectList = Site.Managers.Team.All()
                .Select(team => new SelectListItem()
                {
                    Text = team.Name,
                    Value = team.Id
                }).ToList();

            this.IsMediaContentPresent = true;
        }     
    }
}
