using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Nucleus.Actions;
using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class MaterialGoo : GH_Goo<Material>, ISalamander_Goo
    {
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
                return "Salamander Material";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Material";
            }
        }

        #region Constructors

        public MaterialGoo() : base() { }

        public MaterialGoo(Material material) : base(material) { }

        #endregion

        public override IGH_Goo Duplicate()
        {
            return new MaterialGoo(Value);
        }

        public override string ToString()
        {
            return Value.Name;
        }

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {

        }

        public object GetValue(Type type)
        {
            if (type == typeof(MaterialCollection))
                return new MaterialCollection(Value);
            else return Value;
        }

        public static List<MaterialGoo> Convert(MaterialCollection collection)
        {
            var result = new List<MaterialGoo>();
            if (collection != null)
                foreach (Material obj in collection) result.Add(new MaterialGoo(obj));
            return result;
        }
    }
}
