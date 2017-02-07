using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GetSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{A9E8272A-D553-418F-BD54-FD87F8E5C2E3}");
            }
        }

        public GetSectionComponent()
            : base("GetSection", "Get Section", "Section", "Sections")
        { }
    }
}
