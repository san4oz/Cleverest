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
                .Include("~/Content/Scripts/Libs/jquery-ui.js")
                .Include("~/Content/Scripts/Libs/jquery.carousel.js")
                .Include("~/Content/Scripts/Libs/jquery.validate.js")
                .Include("~/Content/Scripts/Libs/jquery.validate.unobstrusive.js")
                .Include("~/Content/Scripts/Site.js")
                .Include("~/Content/Scripts/Validation.js")
                .Include("~/Content/Scripts/Popup.js")
                .Include("~/Content/Scripts/page.account.teams.js")
                .Include("~/Content/Scripts/page.account.requests.js"));

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
                    .Include("~/Content/Scripts/Libs/FileUpload/jquery.fileupload-ui.js"));


            bundles.Add(new ScriptBundle("~/bundles/admin-scripts")
                .Include("~/Content/Scripts/Admin/page.game.create.js")
                .Include("~/Content/Scripts/Admin/page.game.list.js")
                .Include("~/Content/Scripts/Admin/page.gallery.list.js")
                .Include("~/Content/Scripts/Admin/page.team.list.js")
                .Include("~/Content/Scripts/Admin/page.account.list.js")
                .Include("~/Content/Scripts/Admin/page.question.js"));


            bundles.Add(new StyleBundle("~/bundles/styles")
                .Include("~/Content/Design/bulma.css")
                .Include("~/Content/Design/datepicker.css")
                .Include("~/Content/Design/jquery.fileupload-ui.css")
                .Include("~/Content/Design/jquery.fileupload.css")
                .Include("~/Content/Design/font-awesome.css")
                .Include("~/Content/Design/site.css"));
                
        }
    }
}
