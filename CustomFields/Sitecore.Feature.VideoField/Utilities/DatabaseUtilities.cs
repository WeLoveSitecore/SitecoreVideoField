using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.VideoField.Utilities
{
    public static class DatabaseUtilities
    {
        public static Database ContentOrContextDatabase
        {
            get
            {
                return Sitecore.Context.ContentDatabase ?? Sitecore.Context.Database;
            }
        }
    }
}