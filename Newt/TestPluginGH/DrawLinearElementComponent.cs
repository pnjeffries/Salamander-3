using Newt.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.TestPluginGH
{
    public class DrawLinearElementComponent : NewtBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{9FABBC5F-571A-493E-A082-B3583CCC4155}");
            }
        }

        public DrawLinearElementComponent() :
            base("DrawLinearElement", "Draw Linear Element", "Element", "Model")
        { }
    }
}
