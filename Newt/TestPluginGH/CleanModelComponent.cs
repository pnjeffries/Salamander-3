using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CleanModelComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{8B150347-589F-4C66-9963-C6C049A44396}");

        public CleanModelComponent()
            : base("CleanModel", "Clean Model", "Clean", SubCategories.Params, GH_Exposure.tertiary) { }
    }
}
