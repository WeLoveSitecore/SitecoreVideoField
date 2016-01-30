using Sitecore.Feature.VideoField.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.VideoField.Model
{
    public class BooleanAttribute : VideoAttribute
    {
        string _name = string.Empty;
        string _defaultValue = string.Empty;
        string _value = string.Empty;

        public BooleanAttribute(string name, string defaultValue)
        {
            _name = name;
            _defaultValue = defaultValue;
        }

        public string GetHtmlAttribute()
        {
            if (_value != _defaultValue)
            {
                return _name;
            }
            return string.Empty;
        }

        public void SetAttributeValue(string value)
        {
            _value = value;
        }
    }
}