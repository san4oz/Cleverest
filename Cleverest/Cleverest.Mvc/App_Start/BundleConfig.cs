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
                .Include("~/Content/Scripts/Libs/jquery-3.1.1.js")
                .Include("~/Content/Scripts/Libs/bootstrap.js")
                .Include("~/Content/Scripts/Libs/jquery.custom.js")
                .Include("~/Content/Scripts/Libs/jquery.easing.1.3.js")
                .Include("~/Content/Scripts/Libs/jquery.flexslider.js")
                .Include("~/Content/Scripts/Libs/jquery.prettyPhoto.js")
                .Include("~/Content/Scripts/Libs/jquery.quicksand.js")
                .Include("~/Content/Scripts/Libs/jquery-ui.js")
                .Include("~/Content/Scripts/Libs/jquery.carousel.js")
                .Include("~/Content/Scripts/Site.js")
                .Include("~/Content/Scripts/Popup.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jquery.fileupload")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.ui.widget.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/load-image.all.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/tmpl.min.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/canvas-to-blob.min.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.blueimp-gallery.min.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload-process.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload-image.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload-validate.js")
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload-ui.js")
                );


            bundles.Add(new ScriptBundle("~/bundles/admin-scripts")
                .Include("~/Content/Scripts/Admin/page.game.create.js")
                .Include("~/Content/Scripts/Admin/page.game.list.js")
                .Include("~/Content/Scripts/Admin/page.gallery.list.js")
                .Include("~/Content/Scripts/Admin/page.team.list.js")
                );


            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/Design/bootstrap.css")
                .Include("~/Content/Design/bootstrap-responsive.css")
                .Include("~/Content/Design/prettyPhoto.css")
                .Include("~/Content/Design/flexslider.css")
                .Include("~/Content/Design/custom-styles.css")
                .Include("~/Content/Design/datepicker.css")
                .Include("~/Content/Design/normalize.css")
                .Include("~/Content/Design/jquery.fileupload-ui.css")
                .Include("~/Content/Design/jquery.fileupload.css")
                );
        }
    }
}
