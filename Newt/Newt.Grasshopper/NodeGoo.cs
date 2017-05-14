using FreeBuild.Model;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;
using FreeBuild.Rhino;
using RD = Rhino.Display;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Node Goo
    /// </summary>
    public class NodeGoo : GH_Goo<Node>, ISalamander_Goo, IGH_PreviewData
    {
        #region Properties

        public override bool IsValid
        {
            get
            {
                return Value != null;
            }
        }

        public override string TypeDescription
        {
            get
            {
                return "Salamander Node";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Node";
            }
        }

        public BoundingBox ClippingBox
        {
            get
            {
                Point3d pt = FBtoRC.Convert(Value.Position);
                return new BoundingBox(pt, pt);
            }
        }

        #endregion

        #region Constructor

        public NodeGoo() : base() { }

        public NodeGoo(Node value) : base(value) { }

        #endregion

        #region Methods

        public override IGH_Goo Duplicate()
        {
            return new NodeGoo(Value);
        }

        public override string ToString()
        {
            return "Node " + Value.NumericID;
        }

        public static List<NodeGoo> Convert(NodeCollection collection)
        {
            var result = new List<NodeGoo>();
            if (collection != null)
                foreach (Node obj in collection) result.Add(new NodeGoo(obj));
            return result;
        }

        public object GetValue(Type type)
        {
            if (type == typeof(NodeCollection)) return new NodeCollection(Value);
            else return Value;
        }


        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            args.Pipeline.DrawPoint(FBtoRC.Convert(Value.Position), RD.PointStyle.X, 8, args.Color);
        }

        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            //args.Pipeline.DrawPoint(FBtoRC.Convert(Value.Position), RD.PointStyle.ActivePoint, 1, args.Material.Diffuse);
        }

        public override bool CastTo<Q>(ref Q target)
        {
            if (typeof(Q).IsAssignableFrom(typeof(GH_Point)))
            {
                target = (Q)((object)new GH_Point(FBtoRC.Convert(Value.Position)));
                return true;
            }
            return base.CastTo<Q>(ref target);
        }

        #endregion
    }
}
