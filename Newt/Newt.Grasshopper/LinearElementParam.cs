using Nucleus.Model;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Salamander.Resources;
using Rhino;
using Rhino.DocObjects;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Linear element Grasshopper parameter
    /// </summary>
    public class LinearElementParam : SalamanderPreviewParamBase<LinearElementGoo>, IGH_BakeAwareObject
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

        public bool IsBakeCapable
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Constructor

        public LinearElementParam()
            : base("Linear Element", "Linear Element", "A Salamander Linear Element", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        { }

        public void BakeGeometry(RhinoDoc doc, List<Guid> obj_ids)
        {
            BakeGeometry(doc, null, obj_ids);
        }

        public void BakeGeometry(RhinoDoc doc, ObjectAttributes att, List<Guid> obj_ids)
        {
            foreach (LinearElementGoo goo in m_data)
            {
                Guid id;
                goo.BakeGeometry(doc, att, out id);
            }
        }

        #endregion


    }
}
