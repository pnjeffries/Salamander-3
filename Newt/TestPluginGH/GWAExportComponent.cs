using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GWAExportComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{86182BFE-2D86-4DDD-A844-BD4AB1508DB3}");
            }
        }

        public GWAExportComponent() : base("SaveAsGWA", "Export GWA", "GWA", SubCategories.Export) { }
    }
}
