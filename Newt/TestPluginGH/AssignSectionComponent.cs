using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class AssignSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{1AE47C54-E5FC-4BE9-8899-C58DCBD25179}");
            }
        }

        public AssignSectionComponent()
            : base("AssignSection", "Assign Section", "AssignSect", SubCategories.Model, GH_Exposure.tertiary)
        { }
    }
}
