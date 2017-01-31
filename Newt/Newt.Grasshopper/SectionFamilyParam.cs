using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class SectionFamilyParam : GH_Param<SectionFamilyGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{BFBC0653-28EA-488A-922E-4967CBEE0C7A}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "SectionFamily.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public SectionFamilyParam()
            : base("Section Family", "Section", "Salamander Section Family", "Salamander 3", "Params", GH_ParamAccess.item)
        { }

    }
}
