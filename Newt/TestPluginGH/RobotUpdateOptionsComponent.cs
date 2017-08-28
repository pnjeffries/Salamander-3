using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class RobotUpdateOptionsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{4D46DA8C-3A28-4D1C-B7C2-1DFB3D290AE6}");
            }
        }

        public RobotUpdateOptionsComponent()
            : base("RobotUpdateOptions", "Robot Update Options", "Options", SubCategories.Params, GH_Exposure.tertiary)
        { }
    }
}
