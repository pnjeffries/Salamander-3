using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetBuildUpComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{09A65EA5-C6D6-4204-B1A9-6C564272051C}");
            }
        }

        public GetBuildUpComponent()
            : base("GetBuildUp", "Get Build-Up", "GetBuildUp", SubCategories.BuildUp)
        { }
    }
}
