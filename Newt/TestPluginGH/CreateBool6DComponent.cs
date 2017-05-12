using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateBool6DComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{00B19C2A-1421-4E6C-ADD3-3DF7C7D90299}");
            }
        }

        public CreateBool6DComponent()
            : base("CreateBool6D", "Bool6D", "Bool6D", SubCategories.Model)
        { }
    }
}
