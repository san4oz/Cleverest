using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cleverest.Mvc.ViewModels.Admin
{
    public class BaseInputViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        public bool IsCreateModel { get; set; }

        public bool IsMediaContentPresent { get; set; }

        public IList<string> MediaContent { get;set;}

        public BaseInputViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
