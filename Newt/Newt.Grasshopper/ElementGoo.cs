using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Nucleus.Model;
using Nucleus.Rhino;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class ElementGoo : GH_Goo<Element>, ISalamander_Goo, IGH_PreviewData, IGH_BakeAwareData
    {
        #region Properties

        public override string TypeDescription
        {
            get
            {
                return "Salamander Element";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Element";
            }
        }

        public BoundingBox ClippingBox
        {
            get
            {
                return ToRC.Convert(Value.GetGeometry().BoundingBox);
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
                    mB.AddFamilyPreview(Value);
                    mB.Finalize();
                    _SectionMesh = mB.Mesh;
                }
                return _SectionMesh;
            }
        }

        #endregion

        #region Constructors

        public ElementGoo() : base() { }

        public ElementGoo(Element element) : base(element) { }

        #endregion

        #region Methods

        public override string ToString()
        {
            return "Element " + Value.NumericID;
        }

        public static List<ElementGoo> Convert(ElementCollection collection)
        {
            var result = new List<ElementGoo>();
            if (collection != null)
                foreach (Element obj in collection) result.Add(new ElementGoo(obj));
            return result;
        }

        object ISalamander_Goo.GetValue(Type type)
        {
            if (type == typeof(PanelElementCollection))
                return new PanelElementCollection(Value as PanelElement);
            else if (type == typeof(ElementCollection))
                return new ElementCollection(Value);
            else return Value;
        }

        public void DrawViewportWires(GH_PreviewWireArgs args)
        {
            if (Value?.GetGeometry() != null)
            {
                args.Pipeline.DrawMeshWires(PanelMesh, args.Color);
            }
        }

        public void DrawViewportMeshes(GH_PreviewMeshArgs args)
        {
            //TODO
            if (Value?.GetGeometry() != null)
            {
                args.Pipeline.DrawMeshShaded(PanelMesh, args.Material);
            }
        }

        public override IGH_Goo Duplicate()
        {
            return new ElementGoo(Value);
        }

        public bool BakeGeometry(RhinoDoc doc, ObjectAttributes att, out Guid obj_guid)
        {
            obj_guid = Guid.Empty;
            if (GrasshopperManager.Instance.AutoBake)
                return false;
            else
            {
                var result = Core.Instance.ActiveDocument.Model.Create.CopyOf(Value, null, null);
                obj_guid = Guid.Empty;
                return true;
                //TODO: Bake family
            }
        }

        public override bool CastTo<Q>(ref Q target)
        {
            if (typeof(Q).IsAssignableFrom(typeof(LinearElementGoo)) && Value is LinearElement)
            {
                target = (Q)(object)new LinearElementGoo((LinearElement)Value);
                return true;
            }
            if (typeof(Q).IsAssignableFrom(typeof(PanelElementGoo)) && Value is PanelElement)
            {
                target = (Q)(object)new PanelElementGoo((PanelElement)Value);
                return true;
            }
            return false;
        }

        #endregion
    }
}
