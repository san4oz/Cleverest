using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cleverest.Mvc.ViewModels.Admin.Resources
{
    public class QuestionSelectorViewModel
    {
        [Required]
        [Display(Name = "Гра")]
        public string SelectedGameId { get; set; }

        [Required]
        [Display(Name = "Раунд")]
        public int SelectedRoundNo { get; set; }

        public List<SelectListItem> Games { get; set; }

        public List<SelectListItem> Rounds { get; set; }
    }
}
