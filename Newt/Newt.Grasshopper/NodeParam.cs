using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class NodeParam : GH_Param<NodeGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{C4393420-E9AD-478A-BF03-E95D5ED8E78B}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "Node.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public NodeParam()
            : base("Node", "Node", "A Salamander Node", "Salamander 3", "Params", GH_ParamAccess.item)
        { }
    }
}
