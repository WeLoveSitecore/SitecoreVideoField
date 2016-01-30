
using Sitecore.Feature.VideoField.Interfaces;
using Sitecore.Feature.VideoField.Model;
using System.Collections.Generic;
using System.Text;
using Sitecore.Feature.VideoField.Utilities;
using System.Web;
using System.Linq;

namespace Sitecore.Feature.VideoField
{
    /// <summary>
    /// Helper class for building video source 
    /// </summary>
    public class VideoSource
    {

        public VideoSource()
        {
            Attributes = new List<VideoAttribute>();
            SourceElements = new List<VideoAttribute>();
        }
        ///// <summary>
        ///// The source one
        ///// </summary>
        //public string SourceOne;

        ///// <summary>
        ///// The source two
        ///// </summary>
        //public string SourceTwo;

        ///// <summary>
        ///// The poster
        ///// </summary>
        //public string Poster;

        ///// <summary>
        ///// The width
        ///// </summary>
        //public string Width;

        ///// <summary>
        ///// The height
        ///// </summary>
        //public string Height;

        ///// <summary>
        ///// The controls
        ///// </summary>
        //public string Controls;

        ///// <summary>
        ///// The loop
        ///// </summary>
        //public string Loop;

        ///// <summary>
        ///// The auto play
        ///// </summary>
        //public string Autoplay;

        ///// <summary>
        ///// The muted
        ///// </summary>
        //public string Muted;

        ///// <summary>
        ///// The preload
        ///// </summary>
        //public string Preload;

        public List<VideoAttribute> Attributes
        {
            get; set;
        }

        public List<VideoAttribute> SourceElements
        {
            get; set;
        }

        /// <summary>
        /// Parses the video source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static VideoSource ParseVideoSource(string value)
        {
            string[] videoSourceElements = value.Split('|');
            VideoSource source = new VideoSource();
            source.SourceElements.Add(CreateSourceElement(HttpUtility.HtmlEncode(videoSourceElements[0].GetMediaUrl()), videoSourceElements[0].GetVideoExtension()));
            source.SourceElements.Add(CreateSourceElement(HttpUtility.HtmlEncode(videoSourceElements[1].GetMediaUrl()), videoSourceElements[1].GetVideoExtension()));
            source.Attributes.Add(CreateStringAttribute("poster", "\"" + HttpUtility.HtmlEncode(videoSourceElements[2].GetMediaUrl()) + "\" "));            
            source.Attributes.Add(CreateStringAttribute("width", "\"" + videoSourceElements[3] + "\" "));
            source.Attributes.Add(CreateStringAttribute("height", "\"" + videoSourceElements[4] + "\" "));         
            source.Attributes.Add(CreateBooleanAttribute("controls", videoSourceElements[5]));            
            source.Attributes.Add(CreateBooleanAttribute("loop", videoSourceElements[6]));
            source.Attributes.Add(CreateBooleanAttribute("autoplay", videoSourceElements[7]));
            source.Attributes.Add(CreateBooleanAttribute("muted", videoSourceElements[8]));
            source.Attributes.Add(CreateBooleanAttribute("preload", videoSourceElements[9]));
            return source;
        }

        private static VideoAttribute CreateStringAttribute(string name, string value)
        {
            StringAttribute attr = new StringAttribute(name);
            attr.SetAttributeValue(value);

            return attr;
        }

        private static VideoAttribute CreateBooleanAttribute(string name, string value)
        {
            BooleanAttribute attr = new BooleanAttribute(name, "0");
            attr.SetAttributeValue(value);

            return attr;
        }

        private static VideoAttribute CreateSourceElement(string value, string extension)
        {
            VideoChildSourceTag attr = new VideoChildSourceTag();
            attr.SetAttributeValue(value);
            attr.setExtensionValue(extension);

            return attr;
        }

        public string BuildHTMLVideo()
        {
            if (!this.Attributes.Any())
                return string.Empty;

            StringBuilder video = new StringBuilder();

            video.Append("<video ");
            foreach (VideoAttribute attr in this.Attributes)
            {
                video.Append(attr.GetHtmlAttribute() + " ");

            }
            video.Append(">");
            foreach (VideoAttribute source in this.SourceElements)
            {
                video.Append(source.GetHtmlAttribute());
            }
            video.Append("</video>");

            return video.ToString();
        }
    }
}
