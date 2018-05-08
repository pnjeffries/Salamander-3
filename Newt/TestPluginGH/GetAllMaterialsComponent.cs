using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetAllMaterialsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{6B7BC71E-6B9D-4A79-83E7-B18817BE8983}");

        public GetAllMaterialsComponent()
            : base("GetAllMaterials", "Get All Materials", "GetMaterials", SubCategories.Materials) { }
    }
}
