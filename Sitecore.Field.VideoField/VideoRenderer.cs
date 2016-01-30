namespace Sitecore.Fields
{
    using System.ComponentModel;
    using System.Web.UI;
    using Sitecore.Collections;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Pipelines.RenderField;
    using Sitecore.Web;
    using Sitecore.Web.UI;
    using Sitecore.Web.UI.WebControls;
    using Sitecore.Xml.Xsl;

    /// <summary>
    /// VideoRender 
    /// </summary>
    public class VideoRenderer : WebControl
    {
        /// <summary>
        /// The _render parameters.
        /// </summary>
        private readonly SafeDictionary<string> renderParameters = new SafeDictionary<string>();

        /// <summary>
        /// The after.
        /// </summary>
        private string after = string.Empty;

        /// <summary>
        /// The before.
        /// </summary>
        private string before = string.Empty;

        /// <summary>
        /// The enclosing tag.
        /// </summary>
        private string enclosingTag = string.Empty;

        /// <summary>
        /// The field name.
        /// </summary>
        private string fieldName = string.Empty;

        /// <summary>
        /// The field value.
        /// </summary>
        private string fieldValue = string.Empty;
        /// <summary>
        /// The item to render.
        /// 
        /// </summary>
        private Item item;

        /// <summary>
        /// Gets or sets the after.
        /// </summary>
        /// <value>
        /// The after.
        /// </value>
        [Category("Method")]
        [Description("HTML to render after the field value")]
        public string After
        {
            get
            {
                return this.after;
            }

            set
            {
                Assert.ArgumentNotNull((object)value, "value");
                this.after = value;
            }
        }

        /// <summary>
        /// Gets or sets the before.
        /// </summary>
        /// <value>
        /// The before.
        /// </value>
        [Description("HTML to render before the field value")]
        [Category("Method")]
        public string Before
        {
            get
            {
                return this.before;
            }

            set
            {
                Assert.ArgumentNotNull((object)value, "value");
                this.before = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="T:Sitecore.Web.UI.WebControls.FieldRenderer" /> disables the web editing.
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="T:Sitecore.Web.UI.WebControls.FieldRenderer" /> disables the web editing; otherwise, <c>false</c>.
        /// </value>
        [Category("Method")]
        [Description("Disables the page editor for the control")]
        public bool DisableWebEditing { get; set; }

        /// <summary>
        /// Gets or sets the enclosing tag.
        /// </summary>
        /// <value>
        /// The enclosing tag.
        /// </value>
        [Category("Method")]
        [Description("HTML tag name to wrap the field value with")]
        public string EnclosingTag
        {
            get
            {
                return this.enclosingTag;
            }

            set
            {
                Assert.ArgumentNotNull((object)value, "value");
                this.enclosingTag = value;
            }
        }

        /// <summary>
        /// Gets or sets the field name that will be rendered.
        /// </summary>
        /// <value>
        /// The field name that will be rendered.
        /// </value>
        [Category("Method")]
        [Description("The field name that will be rendered")]
        public string FieldName
        {
            get
            {
                return this.fieldName;
            }

            set
            {
                Assert.ArgumentNotNull((object)value, "value");
                this.fieldName = value;
            }
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item to render.
        /// </value>
        public Item Item
        {
            get
            {
                return this.item;
            }

            set
            {
                Assert.ArgumentNotNull((object)value, "value");
                this.item = value;
            }
        }

        /// <summary>
        /// Gets render parameters. Render parameters are used
        /// to control renderer logic, but are not output themselves.
        /// </summary>
        /// <value>
        /// The render parameters.
        /// </value>
        public SafeDictionary<string> RenderParameters
        {
            get
            {
                return this.renderParameters;
            }
        }

        /// <summary>
        /// Renders the specified item.
        /// </summary>
        /// <param name="item">The item to render.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// The rendered field.
        /// </returns>
        public static string Render(Item item, string fieldName)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)fieldName, "fieldName");
            return FieldRenderer.Render(item, fieldName, string.Empty);
        }

        /// <summary>
        /// Renders the specified item.
        /// </summary>
        /// <param name="item">The item to render.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The rendered field.
        /// </returns>
        public static string Render(Item item, string fieldName, string parameters)
        {
            Assert.ArgumentNotNull((object)item, "item");
            Assert.ArgumentNotNull((object)fieldName, "fieldName");
            Assert.ArgumentNotNull((object)parameters, "parameters");
            FieldRenderer fieldRenderer = new FieldRenderer();
            fieldRenderer.Item = item;
            fieldRenderer.FieldName = fieldName;
            fieldRenderer.Parameters = parameters;
            return fieldRenderer.Render();
        }

        /// <summary>
        /// Gets the text used when tracing.
        /// </summary>
        /// <returns>
        /// The ID or the rendering name with the type name of the control.
        /// </returns>
        public override string GetTraceName()
        {
            return string.Format("FieldRenderer - Field: {0}", (object)StringUtil.GetString(new string[2]
      {
        this.FieldName,
        "Unspecified field"
      }));
        }

        /// <summary>
        /// Overrides the field value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks>
        /// Used by <c>WebEdit</c> infrastructure to render the field "as if" it's value has been changed.
        /// </remarks>
        public void OverrideFieldValue(string value)
        {
            Assert.ArgumentNotNull((object)value, "value");
            this.fieldValue = value;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns>
        /// The render.
        /// </returns>
        public string Render()
        {
            return this.RenderField().ToString();
        }

        /// <summary>
        /// Renders the field.
        /// </summary>
        /// <returns>
        /// The field.
        /// </returns>
        public RenderFieldResult RenderField()
        {
            Item obj = this.Item ?? this.GetItem();
            if (obj == null || obj.Fields[this.FieldName] == null)
            {
                return new RenderFieldResult();
            }

            RenderFieldArgs renderFieldArgs = new RenderFieldArgs()
            {
                After = this.After,
                Before = this.Before,
                EnclosingTag = this.EnclosingTag,
                Item = obj,
                Parameters = WebUtil.ParseQueryString(this.Parameters, true),
                FieldName = this.FieldName,
                RawParameters = this.Parameters,
                RenderParameters = this.RenderParameters,
                DisableWebEdit = this.DisableWebEditing
            };

            if (renderFieldArgs.Parameters["disable-web-editing"] == "true")
            {
                renderFieldArgs.DisableWebEdit = true;
            }

            CorePipeline.Run("renderField", (PipelineArgs)renderFieldArgs);
            return renderFieldArgs.Result;
        }

        /// <summary>
        /// Renders the control.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <remarks>
        /// When developing custom server controls, you can override this method to generate content for an ASP.NET page.
        /// </remarks>
        protected override void DoRender(HtmlTextWriter output)
        {
            Assert.ArgumentNotNull((object)output, "output");
            if (string.IsNullOrEmpty(this.FieldName))
            {
                return;
            }
            output.Write(this.RenderField().ToString());
        }
    }
}
