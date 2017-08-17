using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.BasicToolsGH
{
    public class GetAllLinearElementsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{F853449B-B320-43B3-971D-F0C749FDACDA}");
            }
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        public GetAllLinearElementsComponent()
            :base("GetAllLinearElements", "Get All Linear Elements", "AllLinear", SubCategories.Params)
        {}
    }
}
