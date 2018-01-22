using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateGravityLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{FA5D742C-A224-4FE0-8A0D-62963D6EDABA}");

        public CreateGravityLoadComponent()
            : base("CreateGravityLoad", "Gravity Load", "Gravity", SubCategories.Loading) { }
    }
}
