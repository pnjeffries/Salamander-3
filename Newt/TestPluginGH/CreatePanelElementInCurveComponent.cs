using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreatePanelElementInCurveComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{16B52B0B-27A5-4C09-9A63-1083BA11A72F}");
            }
        }

        public CreatePanelElementInCurveComponent()
            : base("CreatePanelElementInCurve", "Create Panel Element In Curve", "PanelInCrv", SubCategories.Model)
        { }
    }
}
