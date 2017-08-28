using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class MakeElementsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{C094350C-2CFF-4668-8917-02D663C5F19E}");
            }
        }

        public MakeElementsComponent()
            : base("MakeElements", "Convert to Elements", "Convert", SubCategories.Model)
        { }
    }
}
