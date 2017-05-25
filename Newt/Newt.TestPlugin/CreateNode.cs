using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    [Action("CreateNode",
        "Create a new node at a specified position",
        IconBackground = Resources.NodeIconURI,
        IconForeground = Resources.AddIconURI)]
    public class CreateNode : ModelDocumentActionBase
    {
        [ActionInput(1, "the position of the new node")]
        public Vector Position { get; set; }

        [ActionOutput(1, "the created node")]
        public Node Node { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Node = Model.Create.Node(Position, 0, exInfo);
            return true;
        }
    }
}
