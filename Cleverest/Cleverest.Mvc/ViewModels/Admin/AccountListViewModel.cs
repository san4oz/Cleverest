using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cleverest.App_Start.AutoMapper;
using Cleverest.ViewModels;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class AccountListViewModel : BaseViewModel
    {
        [Display(Name = "Пошта")]
        public string Email { get; set; }

        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Команда")]
        public string TeamName { get; set; }
    }
}
