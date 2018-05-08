using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateLinearElementPointLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{B7A9CCF8-D638-447F-B88D-964F591DBB63}");

        public CreateLinearElementPointLoadComponent()
            : base("CreateLinearElementPointLoad", "Linear Element Point Load", "Point Load", SubCategories.Loading) { }
    }
}
