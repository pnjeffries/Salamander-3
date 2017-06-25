using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Base;

namespace Salamander.BasicTools
{
    [Action("CreateBool6D",
        "Create a Bool6D representing six degrees of freedom",
        IconBackground = Resources.URIs.Bool6D,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateBool6DAction : ActionBase
    {
        [ActionInput(1, "the translational degree of freedom along the X-axis")]
        public bool X { get; set; }

        [ActionInput(2, "the translational degree of freedom along the Y-axis")]
        public bool Y { get; set; }

        [ActionInput(3, "the translational degree of freedom along the Z-axis")]
        public bool Z { get; set; }

        [ActionInput(4, "the rotational degree of freedom about the X-axis")]
        public bool XX { get; set; }

        [ActionInput(5, "the rotational degree of freedom about the Y-axis")]
        public bool YY { get; set; }

        [ActionInput(6, "the rotational degree of freedom about the Z-axis")]
        public bool ZZ { get; set; }

        [ActionOutput(1, "the combined 6-Dimensional Boolean")]
        public Bool6D Bool6D { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Bool6D = new Bool6D(X, Y, Z, XX, YY, ZZ);
            return true;
        }
    }
}
