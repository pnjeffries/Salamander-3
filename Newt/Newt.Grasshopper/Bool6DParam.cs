using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class Bool6DParam : GH_Param<Bool6DGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{598FF1E3-DAF3-4B85-A4EA-89B206ED9E8C}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "Bool6D.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public Bool6DParam()
            : base("Bool6D", "Bool6D", "A Six-dimensional boolean", "Salamander 3", "Params", GH_ParamAccess.item)
        { }
    }
}
