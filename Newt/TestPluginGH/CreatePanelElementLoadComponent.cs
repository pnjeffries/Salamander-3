using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreatePanelElementLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{77A22A2E-A3F1-4FAF-A2BC-9F69D3C67258}");

        public CreatePanelElementLoadComponent()
            : base("CreatePanelLoad", "Panel Load", "Pressure", SubCategories.Loading) { }
    }
}
