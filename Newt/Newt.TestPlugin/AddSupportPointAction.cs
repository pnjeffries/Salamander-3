using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Base;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    //Hmmm...?
    [Action("AddSupportPoint",
            "Designate a structural support point.",
        IconBackground = Resources.BaseURI + "LinearElement.png",
        IconForeground = Resources.BaseURI + "AddIcon.png")]
    public class AddSupportPointAction : ModelDocumentActionBase
    {
        [ActionInput(1, "the point to restrain.  The nearest node to this point will be restrained.")]
        public Vector Point { get; set; }

        [ActionInput(2, "the fixity of the support point.")]
        public Bool6D Fixity { get; set; } = new Bool6D(true, true, true, false, false, false);

        [ActionOutput(1, "the support node created ")]
        public Node Node { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Node = Model.Create.Node(Point, 100000000, exInfo);
            Node.SetData(new NodeSupport(Fixity));
            return true;
        }
    }
}
