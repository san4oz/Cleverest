using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Cleverest.Business.Entities;
using Cleverest.Business.Helpers.ImageStorageFactory;
using Cleverest.Business.InterfaceDefinitions.Managers;
using Cleverest.Mvc.Security;
using Cleverest.Mvc.ViewModels.Admin.Resources;

namespace Cleverest.Mvc.Controllers.Admin
{
    public class AccountController : AdminEntityEditorController<Account, AccountInputViewModel>
    {
        protected override ImageStorage ContentStorage
        {
            get
            {
                return ImageStorageFactory.Current.GetStorage(SiteConstants.ImageStorages.Profile);
            }
        }

        protected override IBaseManager<Account> DataManager
        {
            get
            {
                return Site.Managers.Account;
            }
        }
    }
}
