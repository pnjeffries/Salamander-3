using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateBuildUpComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{A91EACF4-A974-4AE8-982A-BE838AD80200}");
            }
        }

        public CreateBuildUpComponent()
            : base("CreateBuildUp", "Create Build-Up", "BuildUp", SubCategories.BuildUp)
        { }
    }
}
