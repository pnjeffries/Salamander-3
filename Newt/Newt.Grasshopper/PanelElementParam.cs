using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class PanelElementParam : SalamanderPreviewParamBase<LinearElementGoo>
    {
        #region Properties

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{A6B7588E-E2E7-4545-9532-F684C3492861}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "PanelElement.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        #endregion

        #region Constructor

        public PanelElementParam()
            : base("Panel Element", "Panel Element", "A Salamander Panel Element", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }

        #endregion
    
    }
}
