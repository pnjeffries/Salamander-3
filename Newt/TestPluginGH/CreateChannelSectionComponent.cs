using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateChannelSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{867AE134-9073-4486-AA31-E193318C6C15}");
            }
        }

        public CreateChannelSectionComponent()
            : base("CreateChannelSection",
                 "Channel Section", "CS", SubCategories.Sections)
        { }
    }
}
