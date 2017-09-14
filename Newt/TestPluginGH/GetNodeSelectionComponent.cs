using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetNodeSelectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{F38CC28A-56B8-4D09-B88C-3752D26CEA27}");

        public GetNodeSelectionComponent()
            : base("GetNodeSelection", "Get Node Selection", "GetNodes", SubCategories.Sets, GH_Exposure.secondary)
        { }
    }
}