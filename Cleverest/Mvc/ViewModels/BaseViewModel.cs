using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cleverest.ViewModels
{
    public class BaseViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        public BaseViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
