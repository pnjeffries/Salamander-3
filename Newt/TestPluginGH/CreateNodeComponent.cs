using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateNodeComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{D03657DC-F408-49F4-9010-A39DF03AAF37}");
            }
        }

        public CreateNodeComponent()
            : base("CreateNode", "Create Node", "Node", SubCategories.Model)
        { }
    }
}
