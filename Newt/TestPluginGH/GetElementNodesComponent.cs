using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetElementNodesComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{74CC992E-EFCC-4551-B8EE-04F92E936703}");
            }
        }

        public GetElementNodesComponent()
            : base("GetElementNodes", "Get Nodes", "Nodes", "Model")
        { }

    }
}
