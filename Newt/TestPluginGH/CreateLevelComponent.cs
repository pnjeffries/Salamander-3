using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.BasicToolsGH
{
    public class CreateLevelComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{01D8D223-E2D8-4CC4-A719-3B950420A561}");

        public CreateLevelComponent() :
            base("CreateLevel", "Level", "Level", SubCategories.Model, GH_Exposure.tertiary)
        { }
    }
}
