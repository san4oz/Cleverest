using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Cleverest.Mvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/jquery-1.10.2.min.js")
                .Include("~/Content/Scripts/Libs/bootstrap.js")
                .Include("~/Content/Scripts/Libs/jquery.custom.js")
                .Include("~/Content/Scripts/Libs/jquery.easing.1.3.js")
                .Include("~/Content/Scripts/Libs/jquery.flexslider.js")
                .Include("~/Content/Scripts/Libs/jquery.prettyPhoto.js")
                .Include("~/Content/Scripts/Libs/jquery.quicksand.js")
                .Include("~/Content/Scripts/Libs/jquery-ui.js")
                .Include("~/Content/Scripts/Site.js")
                );


            bundles.Add(new ScriptBundle("~/bundles/admin-scripts")
                .Include("~/Content/Scripts/Admin/page.game.create.js")
                .Include("~/Content/Scripts/Admin/page.game.list.js")
                );


            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/Design/bootstrap.css")
                .Include("~/Content/Design/bootstrap-responsive.css")
                .Include("~/Content/Design/prettyPhoto.css")
                .Include("~/Content/Design/flexslider.css")
                .Include("~/Content/Design/custom-styles.css")
                .Include("~/Content/Design/datepicker.css")
                .Include("~/Content/Design/normalize.css")
                );
        }
    }
}
