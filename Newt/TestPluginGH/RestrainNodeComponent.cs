using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.BasicToolsGH
{
    public class RestrainNodeComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{9E1447A4-1177-4606-BA39-18954282D1D4}");
            }
        }

        public RestrainNodeComponent()
            : base("RestrainNode", "Restrain Node", "Restrain", SubCategories.Model, GH_Exposure.secondary)
        { }

        
    }
}
