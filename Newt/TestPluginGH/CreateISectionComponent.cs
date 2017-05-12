using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class CreateISectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{5569A197-D16B-42F6-B37E-35750E892099}");
            }
        }

        public CreateISectionComponent()
                : base("CreateISection",
                     "I Section",
                     "IS",
                     SubCategories.Sections)
        { }

    }
}
