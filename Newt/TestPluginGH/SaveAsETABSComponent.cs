using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SaveAsETABSComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{07E8EEA5-6485-4DC2-8771-B2CF6A8AA25E}");

        public SaveAsETABSComponent()
            : base("SaveAsETABS", "Export ETABS", "ETABS", SubCategories.Export) { }
    }
}
