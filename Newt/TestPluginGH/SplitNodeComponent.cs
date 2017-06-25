using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SplitNodeComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{5A60889F-8AA2-4142-9EAD-EE360A77DDD2}");
            }
        }

        public SplitNodeComponent()
            : base("SplitNode", "Split Node", "Split", SubCategories.Model)
        { }
    }
}
