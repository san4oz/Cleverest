using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Helpers.ImageStorageFactory.Storages
{
    public class ProfileImageStorage : ImageStorage
    {
        public const string Name = "Profile";

        protected override string ContentFolderRelativePath
        {
            get
            {
                return "Content/Images/Profile";
            }
        }
    }
}
