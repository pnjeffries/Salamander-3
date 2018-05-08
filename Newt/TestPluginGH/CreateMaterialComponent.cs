using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateMaterialComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{427ACA1C-1B54-4E17-AA65-EB0EC1088AF2}");

        public CreateMaterialComponent()
            : base("CreateMaterial", "Create Material", "Material", SubCategories.Materials) { }
    }
}
