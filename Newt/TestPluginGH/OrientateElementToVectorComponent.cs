using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class OrientateElementToVectorComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{D2A23756-F55D-41F1-9ED6-D304FCE8E78E}");
            }
        }

        public OrientateElementToVectorComponent()
            : base("OrientateElementToVector", "Orientate Towards", "Orientate", SubCategories.Model)
        { }
    }
}
