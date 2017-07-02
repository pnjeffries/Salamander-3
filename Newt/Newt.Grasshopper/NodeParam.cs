using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Salamander.Resources;

namespace Salamander.Grasshopper
{
    public class NodeParam : SalamanderPreviewParamBase<NodeGoo>
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
            : base("Node", "Node", "A Salamander Node", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }

        public override void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            //Do nothing!
        }
    }
}
