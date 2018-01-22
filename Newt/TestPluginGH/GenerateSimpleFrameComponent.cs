using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GenerateSimpleFrameComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{CAD102CD-BCA1-4AFA-AB66-9CFA95CA92F1}");

        public GenerateSimpleFrameComponent()
            : base("GenerateSimpleFrame", "Generate Simple Frame", "Frame", SubCategories.Model)
        { }
    }
}
