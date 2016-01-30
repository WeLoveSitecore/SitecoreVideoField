namespace Sitecore.Fields
{
    using System;
    using System.Text;
    using Sitecore.Diagnostics;
    using Sitecore.Fields.Utilities;
    using Sitecore.Text;
    using Sitecore.Web;
    using Sitecore.Web.UI.HtmlControls;
    using Sitecore.Web.UI.Sheer;

    /// <summary>
    /// Video InputField
    /// </summary>
    public class VideoField : Input
    {
        /// <summary>
        /// The description panel
        /// </summary>
        private Panel descriptionPanel;

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void HandleMessage(Message message)
        {
        //    if (message["id"] == this.ID)
            {
                switch (message.Name)
                {
                    case "video:edit":
                        Sitecore.Context.ClientPage.Start(this, "ShowEditVideo");
                        break;
                    case "video:preview":
                        Sitecore.Context.ClientPage.Start(this, "ShowPreviewVideo");
                        break;
                }
            }
            base.HandleMessage(message);
        }

        /// <summary>
        /// Shows the video.
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected virtual void ShowVideo(ClientPipelineArgs args)
        {
            var urlString = new UrlString(Sitecore.UIUtil.GetUri("control:ShowVideo"));
            UrlHandle handle = new UrlHandle();
            handle["value"] = this.Value;
            handle.Add(urlString);
            SheerResponse.ShowModalDialog(urlString.ToString(), "420px", "500px", string.Empty, true);
            args.WaitForPostBack();
        }

        /// <summary>
        /// Shows the edit schedule.
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected virtual void ShowEditVideo(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (args.IsPostBack)
            {
                if (args.HasResult)
                {
                    if (!string.Equals(args.Result, this.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        this.Value = args.Result;
                        this.SetModified();
                    }
                }
            }
            else
            {
                var urlString = new UrlString(Sitecore.UIUtil.GetUri("control:VideoEditor"));
                UrlHandle handle = new UrlHandle();
                handle["value"] = this.Value;
                handle.Add(urlString);
                SheerResponse.ShowModalDialog(urlString.ToString(), "820px", "600px", string.Empty, true);
                args.WaitForPostBack();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.descriptionPanel = new Panel();
            this.FormatPanel(this.descriptionPanel);
            this.Controls.Add(this.descriptionPanel);
            this.descriptionPanel.ID = this.GetID("DescriptionPanel");
            base.OnInit(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.RefreshDescriptionPanel();
            base.OnLoad(e);
        }

        /// <summary>
        /// Formats the panel.
        /// </summary>
        /// <param name="panel">The panel.</param>
        protected virtual void FormatPanel(Panel panel)
        {
            panel.Background = "white";
            panel.Border = "1px solid #cccccc";
            panel.Padding = "8px 4px 8px 4px";
        }

        /// <summary>
        /// Shows the edit schedule.
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected virtual void ShowPreviewVideo(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (args.IsPostBack)
            {
                if (args.HasResult)
                {
                    if (!string.Equals(args.Result, this.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        this.Value = args.Result;
                        this.SetModified();
                    }
                }
            }
            else
            {
                var urlString = new UrlString(Sitecore.UIUtil.GetUri("control:VideoPreviewEditor"));
                UrlHandle handle = new UrlHandle();
                handle["value"] = this.Value;
                handle.Add(urlString);
                SheerResponse.ShowModalDialog(urlString.ToString(), "420px", "500px", string.Empty, true);
                args.WaitForPostBack();
            }
        }

        /// <summary>
        /// Sets the modified flag.
        /// </summary>
        protected override void SetModified()
        {
            base.SetModified();
            if (base.TrackModified)
            {
                Sitecore.Context.ClientPage.Modified = true;
                this.RefreshDescriptionPanel();
            }
        }

        /// <summary>
        /// Refreshes the description panel.
        /// </summary>
        protected virtual void RefreshDescriptionPanel()
        {
            var builder = new StringBuilder();
            builder.Append("<div style=\"height:80px;\">");
            if (string.IsNullOrEmpty(this.Value))
            {
                builder.Append("No video has been configured.");
            }
            else
            {
                VideoSource videoSource = VideoSource.ParseVideoSource(this.Value);

                builder.Append("&lt;video ");
                if (videoSource.Controls.Equals("1"))
                {
                    builder.Append("controls ");
                }

                if (videoSource.Autoplay.Equals("1"))
                {
                    builder.Append("autoplay ");
                }

                if (!string.IsNullOrEmpty(videoSource.Width))
                {
                    builder.Append("width=\"" + videoSource.Width + "\" ");
                }

                if (!string.IsNullOrEmpty(videoSource.Height))
                {
                    builder.Append("height=\"" + videoSource.Height + "\" ");
                }

                if (videoSource.Loop.Equals("1"))
                {
                    builder.Append("loop ");
                }

                if (videoSource.Muted.Equals("1"))
                {
                    builder.Append("muted ");
                }

                if (videoSource.Preload.Equals("1"))
                {
                    builder.Append("preload=\"" + "auto" + "\" ");
                }
                else
                {
                    builder.Append("preload=\"" + "none" + "\" ");
                }

                if (videoSource.Loop.Equals("1"))
                {
                    builder.Append("loop ");
                }

                if (!string.IsNullOrEmpty(videoSource.Poster))
                {
                    var poster = videoSource.Poster.GetMediaUrl();
                    builder.Append("poster=\"" + poster + "\" ");
                }

                builder.Append("&gt;");
                if (!string.IsNullOrEmpty(videoSource.SourceOne))
                {
                    var sourceOne = videoSource.SourceOne.GetMediaUrl();
                    builder.Append("&lt;src=\"" + sourceOne + "\" type=\"video/"+videoSource.SourceOne.GetVideoExtension()+"\"/&gt;");
                }

                if (!string.IsNullOrEmpty(videoSource.SourceTwo))
                {
                    var sourceTwo = videoSource.SourceTwo.GetMediaUrl();
                    builder.Append("&lt;src=\"" + sourceTwo + "\" type=\"video/"+videoSource.SourceTwo.GetVideoExtension()+"\"/&gt;");
                }

                builder.Append("&lt;/video&gt;");
            }
            builder.Append("</div>");
            this.descriptionPanel.InnerHtml = builder.ToString();
        }
    }
}
