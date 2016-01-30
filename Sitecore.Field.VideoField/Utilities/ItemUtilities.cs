namespace Sitecore.Fields.Utilities
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;

    public static class ItemUtilities
    {
        /// <summary>
        /// Gets the media URL.
        /// </summary>
        /// <param name="itemid">The item ID</param>
        /// <returns>MediaUrl of item</returns>
        public static string GetMediaUrl(this string itemid)
        {
            Item sourceItem =Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(itemid));
            if (sourceItem != null)
            {
                MediaItem mediaItem = new MediaItem(sourceItem);
                return MediaManager.GetMediaUrl(mediaItem);
            }
            return itemid;
        }

        /// <summary>
        /// Gets the media URL.
        /// </summary>
        /// <param name="itemid">The item ID</param>
        /// <returns>MediaUrl of item</returns>
        public static string GetVideoExtension(this string itemid)
        {
            Item sourceItem = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(itemid));
            if (sourceItem != null)
            {
                MediaItem mediaItem = new MediaItem(sourceItem);
                return mediaItem.Extension;
            }
            return itemid;
        }
    }
}
