using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateWallComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{B8EA3722-3259-48E0-8EAB-6D96AC79956A}");

        public CreateWallComponent()
            : base("CreateWall", "Create Wall", "Wall", SubCategories.Model) { }
    }
}
