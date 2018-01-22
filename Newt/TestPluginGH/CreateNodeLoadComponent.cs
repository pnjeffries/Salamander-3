using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateNodeLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{65708CD7-5565-4BAF-B3B8-C24D61F01831}");

        public CreateNodeLoadComponent()
            : base("CreateNodeLoad", "Node Load", "NLoad", SubCategories.Loading) { }
    }
}
