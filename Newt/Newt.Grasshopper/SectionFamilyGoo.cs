using FreeBuild.Model;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.Grasshopper
{
    public class SectionFamilyGoo : GH_Goo<SectionFamily>, ISalamander_Goo
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
                return "Salamander Section Family";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Section Family";
            }
        }

        #region Constructors

        public SectionFamilyGoo() : base() { }

        public SectionFamilyGoo(SectionFamily section) : base(section) { }

        #endregion

        public override IGH_Goo Duplicate()
        {
            return new SectionFamilyGoo(Value);
        }

        public override string ToString()
        {
            return Value.Name;
        }

        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            throw new NotImplementedException();
        }

        public object GetValue(Type type)
        {
            if (type == typeof(SectionFamilyCollection))
                return new SectionFamilyCollection(Value);
            else return Value;
        }
    }
}
