using Grasshopper.Kernel;
using Salamander.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class ElementParam : SalamanderPreviewParamBase<ElementGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{978BAD4A-8309-4D1A-BDD3-8034B32EA4C2}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = Resources.URIs.ParamBackground;
                string uri2 = Resources.URIs.Elements;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public ElementParam()
            : base("Element", "Element", "Salamander Element", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }
    }
}
