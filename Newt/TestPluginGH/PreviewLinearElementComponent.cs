using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class PreviewLinearElementComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{700BBD86-9267-4C7B-86D2-F03C51F4000E}");
            }
        }

        public PreviewLinearElementComponent() :
            base("PreviewLinearElement", "Preview Linear Element", "ElementPreview", SubCategories.Model)
        { }

    }
}

