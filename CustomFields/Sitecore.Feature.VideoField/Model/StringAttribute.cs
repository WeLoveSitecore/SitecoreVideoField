using Sitecore.Feature.VideoField.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.VideoField.Model
{
    public class StringAttribute : VideoAttribute
    {
        string _name = string.Empty;
        string _value = string.Empty;

        public StringAttribute(string name)
        {
            _name = name;
        }

        public string GetHtmlAttribute()
        {
            return _name + "=" + _value;
        }

        public void SetAttributeValue(string name)
        {
            _value = name;
        }


    }
}