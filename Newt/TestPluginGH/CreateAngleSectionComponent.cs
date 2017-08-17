using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateAngleSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{9B1FEC9A-F8A3-4F2A-B28D-67277F11A64E}");
            }
        }

        public CreateAngleSectionComponent()
            : base("CreateAngleSection",
                 "Angle Section",
                 "Angle",
                 SubCategories.Sections)
        { }
    }
}
