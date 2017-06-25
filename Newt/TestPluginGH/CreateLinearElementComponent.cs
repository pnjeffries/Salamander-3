using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.TestPluginGH
{
    public class DrawLinearElementComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{C04BCE6D-8799-4336-8DA6-8F7360759618}");
            }
        }

        public DrawLinearElementComponent() :
            base("CreateLinearElement", "Create Linear Element", "Element", SubCategories.Model)
        { }
        
    }
}
