using Nucleus.Base;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.Grasshopper
{
    public class Bool6DGoo : GH_Goo<Bool6D>, ISalamander_Goo
    {
        #region Properties

        public override bool IsValid
        {
            get
            {
                return true;
            }
        }

        public override string TypeDescription
        {
            get
            {
                return "6-Dimensional Boolean";
            }
        }

        public override string TypeName
        {
            get
            {
                return "Bool6D";
            }
        }

        public override IGH_Goo Duplicate()
        {
            return new Bool6DGoo(Value);
        }

        public override string ToString()
        {
            return Value.ToRestraintDescription();
        }

        object ISalamander_Goo.GetValue(Type type)
        {
            return Value;
        }

        #endregion

        #region Constructors

        public Bool6DGoo() : base() { }

        public Bool6DGoo(Bool6D value) : base(value) { }

        #endregion
    }
}
