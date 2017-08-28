using Grasshopper.Kernel;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SetReleasesComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{632B1395-484C-41A7-A8DF-034C177A7470}");
            }
        }

        public SetReleasesComponent()
            : base("SetReleases", "Set Releases", "Release", SubCategories.Model, GH_Exposure.tertiary)
        { }
    }
}
