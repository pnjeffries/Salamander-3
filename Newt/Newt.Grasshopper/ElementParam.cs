using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class ElementParam : SalamanderPreviewParamBase<ElementGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{978BAD4A-8309-4D1A-BDD3-8034B32EA4C2}");
            }
        }

        public ElementParam()
            : base("Element", "Element", "Salamander Element", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }
    }
}
