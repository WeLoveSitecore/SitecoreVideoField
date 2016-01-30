namespace Sitecore.Fields
{
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using Sitecore.Collections;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Web;
    using Sitecore.Web.UI.WebControls;
    using Sitecore.Xml.Xsl;

    /// <summary>
    /// Video control 
    /// </summary>
    public class Video : FieldControl
    {
        /// <summary>
        /// The _parameters
        /// </summary>
        private SafeDictionary<string> parameters = new SafeDictionary<string>();

        /// <summary>
        /// The _disable web editing
        /// </summary>
        private bool disableWebEditing;

        /// <summary>
        /// The _field
        /// </summary>
        private string field;

        /// <summary>
        /// The _item
        /// </summary>
        private Item item;

        /// <summary>
        /// Gets or sets the data source.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The data source.
        /// </value>
        /// <exception cref="T:System.InvalidOperationException">Cannot set DataSource property, the Item property is already assigned a value.</exception>
        public override string DataSource
        {
            get
            {
                return base.DataSource;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && this.Item != null)
                {
                    throw new InvalidOperationException("Cannot set DataSource property, the Item property is already assigned a value.");
                }

                base.DataSource = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="T:Sitecore.Web.UI.WebControls.FieldControl"/> disables the web editing.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if the <see cref="T:Sitecore.Web.UI.WebControls.FieldControl"/> disables the web editing; otherwise, <c>false</c>.
        /// 
        /// </value>
        public bool DisableWebEditing
        {
            get
            {
                return this.disableWebEditing;
            }

            set
            {
                this.disableWebEditing = value;
            }
        }

        /// <summary>
        /// Gets or sets the field.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The field.
        /// </value>
        public string Field
        {
            get
            {
                return this.field;
            }

            set
            {
                Assert.ArgumentNotNullOrEmpty(value, "field");
                this.field = value;
            }
        }

        /// <summary>
        /// Gets or sets the item.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The item.
        /// </value>
        /// <exception cref="T:System.InvalidOperationException">Cannot set Item property, the DataSource property is already assigned a value.</exception>
        public new Item Item
        {
            get
            {
                return this.item;
            }

            set
            {
                if (value != null && !string.IsNullOrEmpty(this.DataSource))
                {
                    throw new InvalidOperationException("Cannot set Item property, the DataSource property is already assigned a value.");
                }

                this.item = value;
            }
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// 
        /// </summary>
        /// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        /// <remarks>
        /// When developing custom server controls, you can override this method to generate content for an ASP.NET page.
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">Field property is required. All field web controls require the field name to be set.</exception>
        protected override void DoRender(HtmlTextWriter output)
        {
            if (string.IsNullOrEmpty(this.Field))
            {
                throw new InvalidOperationException("Field property is required. All field web controls require the field name to be set.");
            }

            VideoRenderer fieldRenderer = new VideoRenderer();
            fieldRenderer.Item = this.GetItem();
            fieldRenderer.FieldName = this.Field;
            fieldRenderer.Parameters = this.GetParameters();

            fieldRenderer.DisableWebEditing = this.DisableWebEditing;
            RenderFieldResult renderFieldResult = fieldRenderer.RenderField();
            output.Write(renderFieldResult.FirstPart);
            this.RenderChildren(output);
            output.Write(renderFieldResult.LastPart);
        }

        /// <summary>
        /// Gets the item.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        protected override Item GetItem()
        {
            if (this.Item != null)
            {
                return this.Item;
            }

            if (!string.IsNullOrEmpty(this.DataSource))
            {
                return Sitecore.Context.Item.Database.GetItem(this.DataSource);
            }
            else
            {
                return base.GetItem();
            }
        }

        /// <summary>
        /// Populates the parameters.
        /// 
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <remarks>
        /// Inheritors should override the method to provide additional parameters
        /// </remarks>
        protected virtual void PopulateParameters(SafeDictionary<string> parameters)
        {
            Assert.ArgumentNotNull((object)parameters, "parameters");
            this.ParseControlParameters(parameters);
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                parameters.Add("class", this.CssClass);
            }

            if (!string.IsNullOrEmpty(this.CssStyle))
            {
                parameters.Add("style", this.CssStyle);
            }

            if (!string.IsNullOrEmpty(this.Border))
            {
                parameters.Add("border", this.Border);
            }

            if (!this.Height.IsEmpty)
            {
                parameters.Add("height", this.Height.Value.ToString((IFormatProvider)NumberFormatInfo.InvariantInfo));
            }

            if (this.Width.IsEmpty)
            {
                return;
            }

            parameters.Add("width", this.Width.Value.ToString((IFormatProvider)NumberFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Parses the control parameters.
        /// 
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected virtual void ParseControlParameters(SafeDictionary<string> parameters)
        {
            SafeDictionary<string> safeDictionary = WebUtil.ParseQueryString(this.Parameters);
            if (safeDictionary == null)
            {
                return;
            }
            foreach (string key in safeDictionary.Keys)
            {
                if (!string.IsNullOrEmpty(safeDictionary[key]) && !parameters.ContainsKey(key))
                    parameters.Add(key, HttpUtility.UrlDecode(safeDictionary[key]));
            }
        }

        /// <summary>
        /// Gets the parameters in URL string form, to be used by field renderer.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The parameters.
        /// </returns>
        private string GetParameters()
        {
            this.PopulateParameters(this.parameters);
            return WebUtil.BuildQueryString(this.parameters, false, true);
        }
    }
}
