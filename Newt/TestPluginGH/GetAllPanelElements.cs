using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetAllPanelElementsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{4AEE3162-AFC5-4ED7-B9D0-9FDFC70947C3}");
            }
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        public GetAllPanelElementsComponent()
            : base("GetAllPanelElements", "Get All Panel Elements", "AllPanels", SubCategories.Params)
        { }
    }
}
