using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.BasicToolsGH
{
    public class RelaxComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{A959CE52-D2A7-4D85-B6AD-978A4C28B41B}");

        public RelaxComponent()
            : base("Relax", "Relax", "Relax", SubCategories.Analysis, GH_Exposure.hidden) { }
    }
}
