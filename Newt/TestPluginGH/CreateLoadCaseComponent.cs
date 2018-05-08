using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateLoadCaseComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{9EB383D4-55C8-4A41-9DDC-45DF9BEEC836}");

        public CreateLoadCaseComponent()
            : base("CreateLoadCase", "Load Case", "LoadCase", SubCategories.Loading) { }
    }
}
