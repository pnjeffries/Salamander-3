using FreeBuild.Model;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using FreeBuild.Rhino;
using RC = Rhino.Geometry;
using FB = FreeBuild.Geometry;
using Grasshopper.Kernel;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Geometric Goo for Linear Elements
    /// </summary>
    public class LinearElementGoo : GH_Goo<LinearElement>, ISalamander_Goo, IGH_PreviewData
    {
        #region Properties

        //public override BoundingBox Boundingbox
        //{
        //    get
        //    {
        //        return FBtoRC.Convert(Value.Geometry.BoundingBox);
        //    }
        //}

        public override string TypeDescription
        {
            get
            {
                return "Salamander Linear Element";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Linear Element";
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

        public Mesh SectionMesh
        {
            get
            {
                if (_SectionMesh == null)
                {
                    RhinoMeshBuilder mB = new RhinoMeshBuilder();
                    mB.AddSectionPreview(Value);
                    mB.Finalize();
                    _SectionMesh = mB.Mesh;
                }
                return _SectionMesh;
            }
        }

        #endregion

        #region Constructors

        public LinearElementGoo() : base() { }

        public LinearElementGoo(LinearElement element) : base(element) { }

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
            return "Linear Element " + Value.NumericID;
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
                builder.AddSectionPreview(Value);
                builder.Finalize();
                args.Display.DrawMeshShaded(builder.Mesh, args.ShadeMaterial);
            }
        }

        public static List<LinearElementGoo> Convert(LinearElementCollection collection)
        {
            var result = new List<LinearElementGoo>();
            if (collection != null)
                foreach (LinearElement obj in collection) result.Add(new LinearElementGoo(obj));
            return result;
        }

        object ISalamander_Goo.GetValue(Type type)
        {
            if (type == typeof(LinearElementCollection))
                return new LinearElementCollection(Value);
            else if (type == typeof(ElementCollection))
                return new ElementCollection(Value);
            else return Value;
        }

        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            if (Value?.Geometry != null)
            {
                if (Value.Geometry is FB.Line)
                    args.Pipeline.DrawLine(
                        new Line(FBtoRC.Convert(Value.Geometry.StartPoint), FBtoRC.Convert(Value.Geometry.EndPoint)), args.Color);
                else if (Value.Geometry is FB.Arc)
                    args.Pipeline.DrawArc(
                        new Arc(FBtoRC.Convert(Value.Geometry.StartPoint), FBtoRC.Convert(Value.Geometry.PointAt(0.5)), FBtoRC.Convert(Value.Geometry.EndPoint)),
                        args.Color);

                args.Pipeline.DrawMeshWires(SectionMesh, args.Color);
            }
        }

        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            //TODO
            if (Value?.Geometry != null)
            {
                args.Pipeline.DrawMeshShaded(SectionMesh, args.Material);
            }
        }

        public override IGH_Goo Duplicate()
        {
            return new LinearElementGoo(Value);
        }

        #endregion
    }
}
