using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class MergeNodesComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{325BD656-F0F7-457B-BE7F-5D4512784323}");
            }
        }

        public MergeNodesComponent()
            : base("MergeNodes", "Merge Nodes", "Merge", SubCategories.Model, GH_Exposure.secondary)
        { }
    }
}
