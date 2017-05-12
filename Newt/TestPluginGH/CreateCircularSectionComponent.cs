using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{ 
    public class CreateCircularSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{6257C2B4-443C-44CB-92F3-9AB7A7B2EDE3}");
            }
        }

        public CreateCircularSectionComponent()
            : base("CreateCircularSection",
                 "Circular Section",
                 "CS",
                 SubCategories.Sections)
        { }
    }
}
