using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateRectangularHollowSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{E67C6861-1E16-4867-8928-76A624C66D33}");
            }
        }

        public CreateRectangularHollowSectionComponent()
            : base("CreateRectangularHollowSection",
                 "Rectangular Hollow Section",
                 "RHS",
                 SubCategories.Sections)
        { }

    }
}
