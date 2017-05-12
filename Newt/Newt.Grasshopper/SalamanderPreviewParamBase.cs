using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Abstract base class for Salamander parameter components that implements default functionality
    /// for the IGH_PreviewObject interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SalamanderPreviewParamBase<T> : GH_Param<T>, IGH_PreviewObject
        where T : class, IGH_Goo
    {
        private bool _Hidden = false;

        public virtual bool Hidden
        {
            get { return _Hidden; }
            set { _Hidden = value; }
        }

        public virtual bool IsPreviewCapable
        {
            get { return true; }
        }

        public virtual BoundingBox ClippingBox
        {
            get { return Preview_ComputeClippingBox(); }
        }

        private SalamanderPreviewParamBase() : base("Salamander Object", "Salamander Object", "A Salamander Object", "Salamander 3", SubCategories.Params, GH_ParamAccess.item)
        {
        }

        protected SalamanderPreviewParamBase(string name, string nickname, string description, string category, string subCategory, GH_ParamAccess access)
            : base(name, nickname, description, category, subCategory, access)
        { }

        public virtual void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            Preview_DrawMeshes(args);
        }

        public virtual void DrawViewportWires(IGH_PreviewArgs args)
        {
            Preview_DrawWires(args);
        }
    }
}
