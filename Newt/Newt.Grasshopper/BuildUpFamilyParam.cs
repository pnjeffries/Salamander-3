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
    public class BuildUpFamilyParam : GH_Param<BuildUpFamilyGoo>
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{CBB2ADAB-9436-4766-945C-9E2D7B35CF32}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "BuildUpFamily.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public BuildUpFamilyParam()
            : base("Build-Up Family", "BuildUp", "Salamander Build-Up Family", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }

    }
}
