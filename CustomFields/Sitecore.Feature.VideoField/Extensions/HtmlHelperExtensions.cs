using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.VideoField.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString VideoField(this SitecoreHelper helper,string fieldName, Item item)
        {
            return helper.Field(fieldName, item);
        }

    }
}