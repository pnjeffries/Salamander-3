using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class AssignBuildUpComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{06E5C31F-2E66-456F-9C28-7A5F93E726C3}");
            }
        }

        public AssignBuildUpComponent()
            : base("AssignBuildUp", "Assign Build-Up", "AssignBuildUp", SubCategories.Model, GH_Exposure.tertiary)
        { }
    }
}
