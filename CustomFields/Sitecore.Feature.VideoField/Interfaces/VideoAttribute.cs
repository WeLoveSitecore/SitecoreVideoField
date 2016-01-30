using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.VideoField.Interfaces
{
    public interface VideoAttribute
    {
        void SetAttributeValue(String value);

        String GetHtmlAttribute();
    }
}
