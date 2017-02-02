using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cleverest.Mvc.ViewModels.Shared
{
    public class PopupModel
    {
        /// <summary>
        /// Gets or sets the Sana text group code for title.
        /// </summary>
        /// <value>
        /// The Sana text group code for title.
        /// </value>
        public string TitleKey { get; set; }

        /// <summary>
        /// Gets or sets the Sana text group code for description.
        /// </summary>
        /// <value>
        /// The Sana text group code for description.
        /// </value>
        public string DescriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect to when the popup is closed.
        /// </summary>
        /// <value>
        /// The URL to redirect to.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTML with extra buttons to be rendered in the popup.
        /// </summary>
        /// <value>
        /// The HTML with extra buttons.
        /// </value>
        public IHtmlString ExtraButtons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether popup will be open on page load.
        /// </summary>
        /// <value>
        ///   <c>true</c> if popup will be open on page load; otherwise, <c>false</c>.
        /// </value>
        public bool AutoOpen { get; set; }

        /// <summary>
        /// Gets or sets css class for the popup container element.
        /// </summary>
        /// <value>
        /// Css class for the popup container element.
        /// </value>
        public string CssClass { get; set; }

    }
}
