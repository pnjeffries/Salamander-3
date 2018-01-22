using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateThermalLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{FC14D5BC-E29E-4772-9BB5-A83C8ADDA143}");

        public CreateThermalLoadComponent()
            : base("CreateThermalLoad", "Thermal Load", "Thermal Load", SubCategories.Loading) { }
    }
}
