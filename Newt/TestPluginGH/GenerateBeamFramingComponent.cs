using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GenerateBeamFramingComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{A4F78763-503F-4774-9202-06E369BA5994}");

        public GenerateBeamFramingComponent()
            : base("GenerateBeamFraming", "Generate Beam Framing", "GenBeamFraming", SubCategories.Model)
        { }
    }
}
