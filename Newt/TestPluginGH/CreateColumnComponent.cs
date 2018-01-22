using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class CreateColumnComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{1824508B-71D3-4752-89E0-773A84C9C50B}");
            }
        }

        public CreateColumnComponent() :
            base("CreateColumn", "Create Column", "Column", SubCategories.Model)
        { }
        
    }
}
