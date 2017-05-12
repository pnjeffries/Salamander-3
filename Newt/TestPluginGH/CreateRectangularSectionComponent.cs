using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class CreateRectangularSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{AB82100C-6102-4AAE-B4BB-52CCCAE4070A}");
            }
        }

        public CreateRectangularSectionComponent()
            : base("CreateRectangularSection",
                 "Rectangular Section",
                 "RS",
                 SubCategories.Sections)
        { }

    }
}
