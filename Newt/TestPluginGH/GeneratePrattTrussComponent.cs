using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class GeneratePrattTrussComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{B95218C8-FDDC-42DD-9ED6-62B8E4BF5A9B}");
            }
        }

        public GeneratePrattTrussComponent()
            : base("GeneratePrattTruss", "Generate Pratt Truss", "Pratt", SubCategories.Model)
        { }
    }
}
