using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SaveElementSelectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{E1BBB44F-58C9-434E-9F13-F1AF2D509BF5}");

        public SaveElementSelectionComponent()
            : base("SaveElementSelection", "Save Element Selection", "ElementSet", SubCategories.Sets) { }
    }
}
