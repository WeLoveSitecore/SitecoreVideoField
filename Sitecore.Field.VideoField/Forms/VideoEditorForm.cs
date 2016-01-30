namespace Sitecore.Fields.Forms
{
    using System;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Fields.Utilities;
    using Sitecore.Resources.Media;
    using Sitecore.Web;
    using Sitecore.Web.UI.HtmlControls;
    using Sitecore.Web.UI.Pages;
    using Sitecore.Web.UI.Sheer;

    /// <summary>
    /// VideoEditorForm use to edit a form 
    /// </summary>
    public class VideoEditorForm : DialogForm
    {
        /// <summary>
        /// The text source one
        /// </summary>
        private Edit txtSourceOne;

        /// <summary>
        /// The text source two
        /// </summary>
        private Edit txtSourceTwo;

        /// <summary>
        /// The text poster
        /// </summary>
        private Edit txtPoster;

        /// <summary>
        /// The width
        /// </summary>
        private Edit width;

        /// <summary>
        /// The height
        /// </summary>
        private Edit height;

        /// <summary>
        /// The text hidden source one
        /// </summary>
        private Edit txtHiddenSourceOne;

        /// <summary>
        /// The text hidden source two
        /// </summary>
        private Edit txtHiddenSourceTwo;

        /// <summary>
        /// The text hidden poster
        /// </summary>
        private Edit txtHiddenPoster;

        /// <summary>
        /// The CHB automatic play
        /// </summary>
        private Checkbox chbAutoPlay;

        /// <summary>
        /// The CHB loop
        /// </summary>
        private Checkbox chbLoop;

        /// <summary>
        /// The CHB controls
        /// </summary>
        private Checkbox chbControls;

        /// <summary>
        /// The CHB muted
        /// </summary>
        private Checkbox chbMuted;

        /// <summary>
        /// The CHB preload
        /// </summary>
        private Checkbox chbPreload;

        /// <summary>
        /// Called when [browse source one] is clicked.
        /// </summary>
        public virtual void OnBrowseSourceOne()
        {
            ClientPipelineArgs arg = new ClientPipelineArgs();
            arg.Parameters["Source"] = this.txtSourceOne.ID;
            Context.ClientPage.Start(this, "Run", arg);
        }

        /// <summary>
        /// Called when [browse source two] is clicked.
        /// </summary>
        public virtual void OnBrowseSourceTwo()
        {
            ClientPipelineArgs arg = new ClientPipelineArgs();
            arg.Parameters["Source"] = this.txtSourceTwo.ID;
            Context.ClientPage.Start(this, "Run", arg);
        }

        /// <summary>
        /// Called when [browse poster] is clicked.
        /// </summary>
        public virtual void OnBrowsePoster()
        {
            ClientPipelineArgs arg = new ClientPipelineArgs();
            arg.Parameters["Source"] = this.txtPoster.ID;
            Sitecore.Context.ClientPage.Start(this, "Run", arg);
        }

        /// <summary>
        /// Called when [browse source one external].
        /// </summary>
        public virtual void OnBrowseSourceOneExternal()
        {
            ClientPipelineArgs arg = new ClientPipelineArgs();
            arg.Parameters["Source"] = this.txtSourceOne.ID;
            Sitecore.Context.ClientPage.Start(this, "RunExternal", arg);
        }

        /// <summary>
        /// Called when [browse source two external] is clicked.
        /// </summary>
        public virtual void OnBrowseSourceTwoExternal()
        {
            ClientPipelineArgs arg = new ClientPipelineArgs();
            arg.Parameters["Source"] = this.txtSourceTwo.ID;
            Sitecore.Context.ClientPage.Start(this, "RunExternal", arg);
        }

        /// <summary>
        /// Runs the external.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void RunExternal(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                string selectedItemLink = args.Result; //// link to selected item in xml format
                if (args.Parameters["Source"] == this.txtSourceOne.ID)
                {
                    this.txtSourceOne.Value = selectedItemLink.GetAttribute("url");
                    this.txtHiddenSourceOne.Value = selectedItemLink.GetAttribute("url");
                }
                else
                    if (args.Parameters["Source"] == this.txtSourceTwo.ID)
                    {
                        this.txtSourceTwo.Value = selectedItemLink.GetAttribute("url");
                        this.txtHiddenSourceTwo.Value = selectedItemLink.GetAttribute("url");
                    }
                    else
                    {
                        this.txtPoster.Value = selectedItemLink.GetAttribute("url");
                        this.txtHiddenPoster.Value = selectedItemLink.GetAttribute("url");
                    }
            }
            else
            {
                SheerResponse.ShowModalDialog("/sitecore/shell/Applications/Dialogs/External link.aspx", true);
                args.WaitForPostBack();
            }
        }

        /// <summary>
        /// Runs the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Run(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                string selectedItemLink = args.Result;
                if (args.Parameters["Source"] == this.txtSourceOne.ID)
                {
                    this.txtSourceOne.Value = selectedItemLink.GetLinkUrl();
                    this.txtHiddenSourceOne.Value = selectedItemLink.GetAttribute("id");
                }
                else
                    if (args.Parameters["Source"] == this.txtSourceTwo.ID)
                    {
                        this.txtSourceTwo.Value = selectedItemLink.GetLinkUrl();
                        this.txtHiddenSourceTwo.Value = selectedItemLink.GetAttribute("id");
                    }
                    else
                    {
                        this.txtPoster.Value = selectedItemLink.GetLinkUrl();
                        this.txtHiddenPoster.Value = selectedItemLink.GetAttribute("id");
                    }
            }
            else
            {
                SheerResponse.ShowModalDialog("/sitecore/shell/Applications/Dialogs/Media link.aspx", true);
                args.WaitForPostBack();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txtSourceOne.ReadOnly = true;
            this.txtSourceTwo.ReadOnly = true;

            if (!Sitecore.Context.ClientPage.IsEvent)
            {
                var handle = UrlHandle.Get();
                this.SetControlValues(handle["value"]);
            }
        }

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void SetControlValues(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.txtSourceOne.Value = string.Empty;
                this.txtSourceTwo.Value = string.Empty;
                this.txtPoster.Value = string.Empty;
                this.width.Value = string.Empty;
                this.height.Value = string.Empty;
                this.txtHiddenSourceOne.Value = string.Empty;
                this.txtHiddenSourceTwo.Value = string.Empty;
                this.txtHiddenPoster.Value = string.Empty;

                this.chbControls.Value = string.Empty;
                this.chbLoop.Value = string.Empty;
                this.chbAutoPlay.Value = string.Empty;
                this.chbMuted.Value = string.Empty;
                this.chbPreload.Value = string.Empty;
            }
            else
            {
                string[] controlsValue = value.Split('|');
                if (!this.IsGuid(controlsValue[0]))
                {
                    this.txtHiddenSourceOne.Value = controlsValue[0];
                    this.txtSourceOne.Value = controlsValue[0];
                }
                else
                {
                    Item sourceTwoItem = Sitecore.Context.ContentDatabase.GetItem(new ID(controlsValue[0]));
                    if (sourceTwoItem != null)
                    {
                        MediaItem mediaItem = new MediaItem(sourceTwoItem);
                        this.txtHiddenSourceOne.Value = mediaItem.ID.ToString();
                        this.txtSourceOne.Value = MediaManager.GetMediaUrl(mediaItem);
                    }
                }

                if (!this.IsGuid(controlsValue[1]))
                {
                    this.txtHiddenSourceTwo.Value = controlsValue[1];
                    this.txtSourceTwo.Value = controlsValue[1];
                }
                else
                {
                    Item sourceTwoItem = Sitecore.Context.ContentDatabase.GetItem(new ID(controlsValue[1]));
                    if (sourceTwoItem != null)
                    {
                        MediaItem mediaItem = new MediaItem(sourceTwoItem);
                        this.txtHiddenSourceTwo.Value = mediaItem.ID.ToString();
                        this.txtSourceTwo.Value = MediaManager.GetMediaUrl(mediaItem);
                    }
                }


                if (!this.IsGuid(controlsValue[2]))
                {
                    this.txtPoster.Value = controlsValue[2].GetMediaUrl();
                    this.txtHiddenPoster.Value = controlsValue[2];
                }
                else
                {
                    Item posterItem = Sitecore.Context.ContentDatabase.GetItem(new ID(controlsValue[2]));
                    if (posterItem != null)
                    {
                        MediaItem mediaItem = new MediaItem(posterItem);
                        this.txtHiddenPoster.Value = mediaItem.ID.ToString();
                        this.txtPoster.Value = MediaManager.GetMediaUrl(mediaItem);
                    }
                }

              //  this.txtPoster.Value = controlsValue[2];
             //   this.txtHiddenPoster.Value = controlsValue[2];

                this.width.Value = controlsValue[3];
                this.height.Value = controlsValue[4];

                this.chbControls.Value = controlsValue[5];
                this.chbLoop.Value = controlsValue[6];
                this.chbAutoPlay.Value = controlsValue[7];
                this.chbMuted.Value = controlsValue[8];
                this.chbPreload.Value = controlsValue[9];
            }
        }

        /// <summary>
        /// Gets the control values.
        /// </summary>
        /// <returns>Value of controls separated by | </returns>
        protected virtual string GetControlValues()
        {
            var values = new string[10];

            if (!this.IsGuid(this.txtHiddenSourceOne.Value))
            {
                values[0] = this.txtSourceOne.Value;
            }
            else
            {
                Item sourceOneItem = Sitecore.Context.ContentDatabase.GetItem(new ID(this.txtHiddenSourceOne.Value));
                if (sourceOneItem != null)
                {
                    MediaItem mediaItem = new MediaItem(sourceOneItem);
                    values[0] = mediaItem.ID.ToString();
                }
            }

            if (!this.IsGuid(this.txtHiddenSourceTwo.Value))
            {
                values[1] = this.txtSourceTwo.Value;
            }
            else
            {
                Item sourceTwoItem = Sitecore.Context.ContentDatabase.GetItem(new ID(this.txtHiddenSourceTwo.Value));
                if (sourceTwoItem != null)
                {
                    MediaItem mediaItem = new MediaItem(sourceTwoItem);
                    values[1] = mediaItem.ID.ToString();
                }
            }

            if (this.IsGuid(this.txtHiddenPoster.Value))
            {
                Item sourcePosterItem = Sitecore.Context.ContentDatabase.GetItem(new ID(this.txtHiddenPoster.Value));
                if (sourcePosterItem != null)
                {
                    MediaItem mediaItem = new MediaItem(sourcePosterItem);
                    values[2] = mediaItem.ID.ToString();
                }
            }

            values[3] = this.width.Value;
            values[4] = this.height.Value;
            values[5] = this.chbControls.Checked ? "1" : "0";
            values[6] = this.chbLoop.Checked ? "1" : "0";
            values[7] = this.chbAutoPlay.Checked ? "1" : "0";
            values[8] = this.chbMuted.Checked ? "1" : "0";
            values[9] = this.chbPreload.Checked ? "1" : "0";
            return string.Join("|", values);
        }

        /// <summary>
        /// Called when [ok].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            var value = this.GetControlValues();
            SheerResponse.SetDialogValue(value);
            base.OnOK(sender, args);
        }

        /// <summary>
        /// Called when [clear] button is clicked.
        /// </summary>
        protected virtual void OnClear()
        {
            var args = Sitecore.Context.ClientPage.CurrentPipelineArgs as ClientPipelineArgs;
            Assert.IsNotNull(args, typeof(ClientPipelineArgs));
            if (args.IsPostBack)
            {
                if (args.Result == "yes")
                {
                    this.SetControlValues(null);
                }
            }
            else
            {
                SheerResponse.Confirm("Are you sure you want to clear all of the settings?");
                args.WaitForPostBack();
            }
        }

        /// <summary>
        /// Determines whether the specified unique identifier string is unique identifier.
        /// </summary>
        /// <param name="guidStr">The unique identifier string.</param>
        /// <returns>true if is GUID</returns>
        private bool IsGuid(string guidStr)
        {
            Guid guid;
            return Guid.TryParse(guidStr, out guid);
        }
    }
}
