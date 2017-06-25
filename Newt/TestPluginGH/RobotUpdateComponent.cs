using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class RobotUpdateComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{EA3A1F9D-CFC7-43D8-85F6-754974B437DC}");
            }
        }

        public RobotUpdateComponent()
            : base("UpdateRobot", "Update Robot", "UpdateRobo", SubCategories.Export)
        { }
    }
}
