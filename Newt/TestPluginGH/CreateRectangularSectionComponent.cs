using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class CreateRectangularSectionComponent : NewtBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{889830A0-0121-4A40-89C3-B7771B52903F}");
            }
        }

        public CreateRectangularSectionComponent()
            : base("CreateRectangularSection",
                 "Rectangular Section",
                 "RS",
                 "Sections")
        { }

    }
}
