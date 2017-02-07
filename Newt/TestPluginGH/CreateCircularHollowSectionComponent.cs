using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateCircularHollowSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{3E0A4F78-695A-4DD7-9088-178D7CFB9DF4}");
            }
        }

        public CreateCircularHollowSectionComponent()
            : base("CreateCircularHollowSection",
                 "Circular Hollow Section",
                 "CHS",
                 "Sections")
        { }
    }
}
