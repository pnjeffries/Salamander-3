using Nucleus.Model;
using Nucleus.Rhino;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    class PanelElementGoo : GH_Goo<PanelElement>, ISalamander_Goo, IGH_PreviewData
    {
        #region Properties

        public override string TypeDescription
        {
            get
            {
                return "Salamander Panel Element";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Panel Element";
            }
        }

        public BoundingBox ClippingBox
        {
            get
            {
                return FBtoRC.Convert(Value.Geometry.BoundingBox);
            }
        }

        public override bool IsValid
        {
            get
            {
                return Value != null;
            }
        }

        private Mesh _SectionMesh = null;

        public Mesh PanelMesh
        {
            get
            {
                if (_SectionMesh == null)
                {
                    RhinoMeshBuilder mB = new RhinoMeshBuilder();
                    mB.AddPanelPreview(Value);
                    mB.Finalize();
                    _SectionMesh = mB.Mesh;
                }
                return _SectionMesh;
            }
        }

        #endregion

        #region Constructors

        public PanelElementGoo() : base() { }

        public PanelElementGoo(PanelElement element) : base(element) { }

        #endregion

        #region Methods

        //public override IGH_GeometricGoo DuplicateGeometry()
        //{
        //    return new LinearElementGoo(Value);
        //}

        //public override BoundingBox GetBoundingBox(Transform xform)
        //{
        //    RC.Curve rCrv = FBtoRC.Convert(Value.Geometry);
        //    if (rCrv != null)
        //    {
        //        rCrv.Transform(xform);
        //        return rCrv.GetBoundingBox(false);
        //    }
        //    else return BoundingBox.Unset;
        //}

        //public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        //{
        //    throw new NotImplementedException();
        //}

        public override string ToString()
        {
            return "Panel Element " + Value.NumericID;
        }

        //public override IGH_GeometricGoo Transform(Transform xform)
        //{
        //    throw new NotImplementedException();
        //}

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            if (Value != null)
            {
                RhinoMeshBuilder builder = new RhinoMeshBuilder();
                builder.AddPanelPreview(Value);
                builder.Finalize();
                args.Display.DrawMeshShaded(builder.Mesh, args.ShadeMaterial);
            }
        }

        public static List<PanelElementGoo> Convert(PanelElementCollection collection)
        {
            var result = new List<PanelElementGoo>();
            if (collection != null)
                foreach (PanelElement obj in collection) result.Add(new PanelElementGoo(obj));
            return result;
        }

        object ISalamander_Goo.GetValue(Type type)
        {
            if (type == typeof(PanelElementCollection))
                return new PanelElementCollection(Value);
            else if (type == typeof(ElementCollection))
                return new ElementCollection(Value);
            else return Value;
        }

        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            if (Value?.Geometry != null)
            {
                /*if (Value.Geometry is FB.Line)
                    args.Pipeline.DrawLine(
                        new Line(FBtoRC.Convert(Value.Geometry.StartPoint), FBtoRC.Convert(Value.Geometry.EndPoint)), args.Color);
                else if (Value.Geometry is FB.Arc)
                    args.Pipeline.DrawArc(
                        new Arc(FBtoRC.Convert(Value.Geometry.StartPoint), FBtoRC.Convert(Value.Geometry.PointAt(0.5)), FBtoRC.Convert(Value.Geometry.EndPoint)),
                        args.Color);*/

                args.Pipeline.DrawMeshWires(PanelMesh, args.Color);
            }
        }

        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            //TODO
            if (Value?.Geometry != null)
            {
                args.Pipeline.DrawMeshShaded(PanelMesh, args.Material);
            }
        }

        public override IGH_Goo Duplicate()
        {
            return new PanelElementGoo(Value);
        }

        #endregion
    
    }
}
