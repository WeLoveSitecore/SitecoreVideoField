namespace Sitecore.Feature.VideoField.Forms
{
    using System;
    using Sitecore.Feature.VideoField.Utilities;
    using Sitecore.Web;
    using Sitecore.Web.UI.HtmlControls;
    using Sitecore.Web.UI.Pages;

    /// <summary>
    /// VideoPreviewForm for previewing video field
    /// </summary>
    public class VideoPreviewForm : DialogForm
    {
        /// <summary>
        /// The video source
        /// </summary>
        private Literal videoSource;
     
        /// <summary>
        /// Raises the load event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        /// This method notifies the server control that it should perform actions common to each HTTP
        /// request for the page it is associated with, such as setting up a database query. At this
        /// stage in the page lifecycle, server controls in the hierarchy are created and initialized,
        /// view state is restored, and form controls reflect client-side data. Use the IsPostBack
        /// property to determine whether the page is being loaded in response to a client postback,
        /// or if it is being loaded and accessed for the first time.
        /// </remarks>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Sitecore.Context.ClientPage.IsEvent)
            {
                var handle = UrlHandle.Get();
                /// set 
                this.SetControlValues(handle["value"]);
            }
        }

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void SetControlValues(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ////parse the fieldvalue and build video tag
                string[] controlsValue = value.Split('|');
                var sourceOne = controlsValue[0].GetMediaUrl();
                var sourceOneExtension = "video/" + controlsValue[0].GetVideoExtension();
                
                var sourceTwo = controlsValue[1].GetMediaUrl();
                var sourceTwoExtension ="video/"+ controlsValue[1].GetVideoExtension();
                

                string poster = string.Empty;
                if (!string.IsNullOrEmpty(controlsValue[2]))
                {
                    poster = "poster='" + controlsValue[2].GetMediaUrl() + "' ";
                }

                var width = controlsValue[3];
                var height = controlsValue[4];

                var hasControls = controlsValue[5].Equals("1") ? "controls" : string.Empty;
                var hasLoop = controlsValue[6].Equals("1") ? "loop" : string.Empty;
                var autoPlay = controlsValue[7].Equals("1") ? "autoplay" : string.Empty;
                var muted = controlsValue[8].Equals("1") ? "muted" : string.Empty;
                var preload = controlsValue[9].Equals("1") ? "preload" : string.Empty;

                this.videoSource.Text = string.Format("<video width='{0}' height='{1}' {2} {3} {4} {5} {6} {7}><source src='{8}' type='{9}'>", width, height, hasControls, hasLoop, autoPlay, muted, preload, poster, sourceOne,sourceOneExtension);
                this.videoSource.Text += string.Format("<source src='{0}' type='{1}'>Your browser does not support the video tag.</video>", sourceTwo,sourceTwoExtension);
            }
        }
    }
}