using FreeBuild.Model;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Node Goo
    /// </summary>
    public class NodeGoo : GH_Goo<Node>
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

        #endregion
    }
}
