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
    public class MaterialParam : GH_Param<MaterialGoo>
    {
        public override Guid ComponentGuid => new Guid("{CD977792-A31A-4214-9840-15E0CC0A899F}");

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = URIs.ParamBackground;
                string uri2 = URIs.Material;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public MaterialParam()
            : base("Material", "Material", "Salamander Material", SubCategories.Category, SubCategories.Params, GH_ParamAccess.item) { }
    }
}
