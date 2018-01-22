using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class CreateVectorNodeLoadComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid => new Guid("{5D61FD31-6FDA-45BB-A9F9-88340D5D92EE}");

        public CreateVectorNodeLoadComponent()
            : base("CreateVectorNodeLoad", "Vector Node Load", "VectNLoad", SubCategories.Loading) { }
    }
}
