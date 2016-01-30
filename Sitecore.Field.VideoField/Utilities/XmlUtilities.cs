namespace Sitecore.Fields.Utilities
{
    using System;
    using System.IO;
    using System.Xml;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Resources.Media;

    /// <summary>
    /// Helper class for xml
    /// </summary>
    public static class XmlUtilities
    {
        /// <summary>
        /// The _attributes
        /// </summary>
        private static SafeDictionary<string> attributes;

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="fieldValue">The field value.</param>
        /// <param name="name">The name.</param>
        /// <returns>Value of the attribute</returns>
        public static string GetAttribute(this string fieldValue, string name)
        {
            Assert.ArgumentNotNullOrEmpty(name, "name");
            EnsureAttributes(fieldValue);
            return StringUtil.GetString(new string[1]
            {
            attributes[name]
            });
        }

        /// <summary>
        /// Gets the link URL.
        /// </summary>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>Link url</returns>
        public static string GetLinkUrl(this string fieldValue)
        {
            string id = fieldValue.GetAttribute("id");
            if (!string.IsNullOrEmpty(id))
            {
                MediaItem imageItem = Sitecore.Context.ContentDatabase.GetItem(new ID(id));
                if (imageItem != null)
                { return imageItem.MediaPath; }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the link from string.
        /// </summary>
        /// <param name="idValue">The identifier value.</param>
        /// <param name="database">The database.</param>
        /// <returns></returns>
        public static string GetLinkFromStr(string idValue, string database)
        {
            string link = string.Empty;
            try
            {
                ID id = new ID(idValue);
                Item mediaItem = Sitecore.Configuration.Factory.GetDatabase(database).GetItem(id);
                if (mediaItem != null)
                {
                    return MediaManager.GetMediaUrl(mediaItem);
                }
            }
            catch (FormatException)
            {
                return string.Empty;
            }

            return link;
        }

        /// <summary>
        /// Ensures the _attributes.
        /// 
        /// </summary>
        private static void EnsureAttributes(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            attributes = new SafeDictionary<string>();
            try
            {
                XmlTextReader xmlTextReader = new XmlTextReader((TextReader)new StringReader(s));
                while (xmlTextReader.Read())
                {
                    int num = (int)xmlTextReader.MoveToContent();
                    if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.MoveToFirstAttribute())
                    {
                        do
                        {
                            attributes[xmlTextReader.Name] = xmlTextReader.Value;
                        }
                        while (xmlTextReader.MoveToNextAttribute());
                        xmlTextReader.MoveToElement();
                    }
                }
            }
            catch (XmlException ex)
            {
            }
        }
    }
}
