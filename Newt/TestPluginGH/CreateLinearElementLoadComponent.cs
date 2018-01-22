using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateLinearElementLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{A8E411C8-120C-4076-99BB-2ED0C6301E57}");

        public CreateLinearElementLoadComponent()
            : base("CreateLinearElementLoad", "Linear Element Load", "UDL", SubCategories.Loading) { }
    }
}
