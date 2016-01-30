namespace Sitecore.Feature.VideoField.Utilities
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Resources.Media;
    using Sitecore.Feature.VideoField.Utilities;
    using System;
    using System.Linq;
    public static class ItemUtilities
    {
        public static string GetMediaUrl(this string itemid)
        {
            if (!string.IsNullOrEmpty(itemid))
            {
                Guid guidOutput = new Guid();
                bool isValid = Guid.TryParse(itemid, out guidOutput);
                if (isValid)
                {
                    Item sourceItem = DatabaseUtilities.ContentOrContextDatabase.GetItem(new ID(itemid));
                    if (sourceItem != null)
                    {
                        MediaItem mediaItem = new MediaItem(sourceItem);
                        return MediaManager.GetMediaUrl(mediaItem);
                    }
                }
                else
                {
                    return itemid;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the media URL.
        /// </summary>
        /// <param name="itemid">The item ID</param>
        /// <returns>MediaUrl of item</returns>
        public static string GetVideoExtension(this string itemid)
        {
            if (!string.IsNullOrEmpty(itemid))
            {
                Guid guidOutput = new Guid();
                bool isValid = Guid.TryParse(itemid, out guidOutput);
                if (isValid)
                {
                    Item sourceItem = DatabaseUtilities.ContentOrContextDatabase.GetItem(new ID(itemid));
                    if (sourceItem != null)
                    {
                        MediaItem mediaItem = new MediaItem(sourceItem);
                        return mediaItem.Extension;
                    }
                    return itemid;
                }
                else
                {
                    string[] items = itemid.Split('.');
                    return items.LastOrDefault();
                }
            }

            return string.Empty;
        }
    }
}