using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreatePanelElementComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{D0E3AAFA-35B0-4276-B1B9-0DA3CDD49413}");
            }
        }

        public CreatePanelElementComponent()
            : base("CreatePanelElement", "Create Panel Element", "Panel", SubCategories.Model)
        { }
    }
}
