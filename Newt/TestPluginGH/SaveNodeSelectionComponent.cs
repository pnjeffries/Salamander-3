using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SaveNodeSelectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{D9ABC3CC-0F3E-4C46-B31B-114204839D0C}");

        public SaveNodeSelectionComponent()
            : base("SaveNodeSelection", "Save Node Selection", "NodeSet", SubCategories.Sets) { }
    }
}
