using Nucleus.Model;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    public class BuildUpFamilyGoo : GH_Goo<BuildUpFamily>, ISalamander_Goo
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
                return "Salamander Build-Up Family";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Build-Up Family";
            }
        }

        #region Constructors

        public BuildUpFamilyGoo() : base() { }

        public BuildUpFamilyGoo(BuildUpFamily section) : base(section) { }

        #endregion

        public override IGH_Goo Duplicate()
        {
            return new BuildUpFamilyGoo(Value);
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
            if (type == typeof(BuildUpFamilyCollection))
                return new BuildUpFamilyCollection(Value);
            else return Value;
        }
}
}
