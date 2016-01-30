namespace Sitecore.Feature.VideoField
{
    using System.Text;
    using System.Web;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.VideoField.Utilities;
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
            
            return videoSource.BuildHTMLVideo();
        }
    }
}
