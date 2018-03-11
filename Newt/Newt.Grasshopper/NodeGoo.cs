using Nucleus.Model;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Nucleus.Rhino;
using RD = Rhino.Display;
using Nucleus.Actions;
using Rhino;
using Rhino.DocObjects;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Node Goo
    /// </summary>
    public class NodeGoo : GH_Goo<Node>, ISalamander_Goo, IGH_PreviewData, IGH_BakeAwareData
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
                Point3d pt = ToRC.Convert(Value.Position);
                return new BoundingBox(pt, pt);
            }
        }

        /// <summary>
        /// Private backing field for ExInfo property
        /// </summary>
        private ExecutionInfo _ExInfo;

        /// <summary>
        /// The execution information for the operation which created this object
        /// </summary>
        public ExecutionInfo ExInfo
        {
            get { return _ExInfo; }
        }

        private Mesh _SupportMesh = null;

        public Mesh SupportMesh
        {
            get
            {
                if (_SupportMesh == null)
                {
                    if (Value.HasData<NodeSupport>())
                    {
                        NodeSupport support = Value.GetData<NodeSupport>();
                        if (!support.Fixity.AllFalse)
                        {
                            RhinoMeshBuilder mB = new RhinoMeshBuilder();
                            mB.AddNodeSupport(Value, support);
                            mB.Finalize();
                            _SupportMesh = mB.Mesh;
                        }
                    }
                }
                return _SupportMesh;
            }
        }

        #endregion

        #region Constructor

        public NodeGoo() : base() { }

        public NodeGoo(Node value, ExecutionInfo exInfo) : base(value)
        {
            _ExInfo = exInfo;
        }

        #endregion

        #region Methods

        public override IGH_Goo Duplicate()
        {
            return new NodeGoo(Value, _ExInfo);
        }

        public override string ToString()
        {
            return "Node " + Value.NumericID;
        }

        public static List<NodeGoo> Convert(NodeCollection collection, ExecutionInfo exInfo)
        {
            var result = new List<NodeGoo>();
            if (collection != null)
                foreach (Node obj in collection) result.Add(new NodeGoo(obj, exInfo));
            return result;
        }

        public object GetValue(Type type)
        {
            if (type == typeof(NodeCollection)) return new NodeCollection(Value);
            else return Value;
        }


        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            if (Value != null)
            {
                args.Pipeline.DrawPoint(ToRC.Convert(Value.Position), RD.PointStyle.X, 8, args.Color);
                /*Mesh mesh = SupportMesh;
                if (mesh != null) args.Pipeline.DrawMeshWires(mesh, args.Color);*/
            }
        }


        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            /*if (Value != null)
            {
                Mesh mesh = SupportMesh;
                if (mesh != null) args.Pipeline.DrawMeshShaded(mesh, args.Material);
            }*/
        }

        public override bool CastTo<Q>(ref Q target)
        {
            if (typeof(Q).IsAssignableFrom(typeof(GH_Point)))
            {
                target = (Q)((object)new GH_Point(ToRC.Convert(Value.Position)));
                return true;
            }
            return base.CastTo<Q>(ref target);
        }

        public bool BakeGeometry(RhinoDoc doc, ObjectAttributes att, out Guid obj_guid)
        {
            if (GrasshopperManager.Instance.AutoBake)
            {
                obj_guid = Guid.Empty;
                return false;
            }
            else
            {
                var result = Core.Instance.ActiveDocument.Model.Create.CopyOf(Value, null);
                obj_guid = Guid.Empty;
                return true;
            }
        }

        #endregion
    }
}
