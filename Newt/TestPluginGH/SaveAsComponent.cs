using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicToolsGH
{
    public class SaveAsComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{91D083B9-7865-4C9D-AED0-C8EF57B4471E}");
            }
        }

        public SaveAsComponent()
            : base("SaveAs", "Save Salamander 3 File", "SaveS3b", SubCategories.Export)
        { }
    }
}
