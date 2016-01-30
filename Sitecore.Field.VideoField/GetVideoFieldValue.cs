namespace Sitecore.Fields
{
    using System.Text;
    using System.Web;
    using Sitecore.Diagnostics;
    using Sitecore.Fields.Utilities;
    using Sitecore.Pipelines.RenderField;

    /// <summary>
    ///  Process Video Field
    /// </summary>
    public class GetVideoFieldValue
    {
        /// <summary>
        /// Processes the specified arguments,get the field value.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(RenderFieldArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");
            string fieldTypeKey = args.FieldTypeKey;
            if (fieldTypeKey != "video")
            {
                return;
            }

            args.Result.FirstPart = BuildVideoTag(args.Result.FirstPart);
        }

        /// <summary>
        /// Builds the video tag.
        /// </summary>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>Video tag</returns>
        private string BuildVideoTag(string fieldValue)
        {
            VideoSource videoSource = VideoSource.ParseVideoSource(fieldValue);
            /// set video tag _attributes 
            var builder = new StringBuilder();
            if (videoSource != null)
            {
                builder.Append("<video ");
                if (!string.IsNullOrEmpty(videoSource.Controls))
                {
                    builder.Append("controls='' ");
                }

                if (videoSource.Autoplay.Equals("1"))
                {
                    builder.Append("autoplay='' ");
                }

                if (videoSource.Loop.Equals("1"))
                {
                    builder.Append("loop='' ");
                }

                if (videoSource.Loop.Equals("1"))
                {
                    builder.Append("loop='' ");
                }

                if (videoSource.Muted.Equals("1"))
                {
                    builder.Append("Muted='' ");
                }

                if (!string.IsNullOrEmpty(videoSource.Width))
                {
                    builder.Append("width=\"" + videoSource.Width + "\" ");
                }

                if (!string.IsNullOrEmpty(videoSource.Height))
                {
                    builder.Append("height=\"" + videoSource.Height + "\" ");
                }

                if (!string.IsNullOrEmpty(videoSource.Poster))
                {
                    builder.Append("poster=\"" + HttpUtility.HtmlEncode(videoSource.Poster.GetMediaUrl()) + "\" ");
                }

                builder.Append(">");

                ///set source for video 
                if (!string.IsNullOrEmpty(videoSource.SourceOne))
                {
                    builder.Append("<source src=\"" + HttpUtility.HtmlEncode(videoSource.SourceOne.GetMediaUrl()) + "\" type=\"video/" + videoSource.SourceOne.GetVideoExtension() + "\"/>");
                }

                if (!string.IsNullOrEmpty(videoSource.SourceTwo))
                {
                    builder.Append("<source src=\"" + HttpUtility.HtmlEncode(videoSource.SourceTwo.GetMediaUrl()) + "\" type=\"video/" + videoSource.SourceTwo.GetVideoExtension() + "\"/>");
                }

                builder.Append("</video>");
            }

            return builder.ToString();
        }
    }
}
