
namespace Sitecore.Fields
{
    /// <summary>
    /// Helper class for building video source 
    /// </summary>
    public class VideoSource
    {
        /// <summary>
        /// The source one
        /// </summary>
        public string SourceOne;

        /// <summary>
        /// The source two
        /// </summary>
        public string SourceTwo;

        /// <summary>
        /// The poster
        /// </summary>
        public string Poster;

        /// <summary>
        /// The width
        /// </summary>
        public string Width;

        /// <summary>
        /// The height
        /// </summary>
        public string Height;

        /// <summary>
        /// The controls
        /// </summary>
        public string Controls;

        /// <summary>
        /// The loop
        /// </summary>
        public string Loop;

        /// <summary>
        /// The auto play
        /// </summary>
        public string Autoplay;

        /// <summary>
        /// The muted
        /// </summary>
        public string Muted;

        /// <summary>
        /// The preload
        /// </summary>
        public string Preload;

        /// <summary>
        /// Parses the video source.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static VideoSource ParseVideoSource(string value)
        {
            string[] videoSourceElements = value.Split('|');
            VideoSource source = new VideoSource();

            
            source.SourceOne = videoSourceElements[0];
            source.SourceTwo = videoSourceElements[1];
            source.Poster = videoSourceElements[2];
            source.Width = videoSourceElements[3];
            source.Height = videoSourceElements[4];
            source.Controls = videoSourceElements[5];
            source.Loop = videoSourceElements[6];
            source.Autoplay = videoSourceElements[7];
            source.Muted = videoSourceElements[8];
            source.Preload = videoSourceElements[9];
            return source;
        }
    }
}
