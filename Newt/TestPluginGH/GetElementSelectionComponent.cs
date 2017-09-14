using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetElementSelectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{C571C977-73A8-46C1-A5CD-33D34719ECCC}");

        public GetElementSelectionComponent()
            : base("GetElementSelection", "Get Element Selection", "GetElements", SubCategories.Sets, GH_Exposure.secondary)
        { }
    }
}
