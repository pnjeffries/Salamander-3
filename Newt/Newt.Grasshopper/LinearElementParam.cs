using FreeBuild.Model;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Linear element Grasshopper parameter
    /// </summary>
    public class LinearElementParam : GH_Param<LinearElementGoo>
    {
        #region Properties

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{6B1E89C3-6198-4B7A-AD47-AB88358EEF50}");
            }
        }

        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "LinearElement.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        #endregion

        #region Constructor

        public LinearElementParam()
            : base("Linear Element", "Linear Element", "A Salamander Linear Element", "Salamander 3", "Params", GH_ParamAccess.item)
        { }

        #endregion


    }
}
