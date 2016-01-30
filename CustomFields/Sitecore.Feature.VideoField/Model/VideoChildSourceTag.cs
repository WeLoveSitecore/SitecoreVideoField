using Sitecore.Feature.VideoField.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.VideoField.Model
{
    public class VideoChildSourceTag : VideoAttribute
    {
        string _value = string.Empty;
        string _extension = string.Empty;

        public VideoChildSourceTag()
        {
        }

        public string GetHtmlAttribute()
        {
            return "<source src =\"" + _value + "\" type=\"video/" + _extension + "\"/>";
        }

        public void SetAttributeValue(string value)
        {
            _value = value ;
        }

        public void setExtensionValue(string value)
        {
            _extension = value;
        }
    }
}