using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    [Action("MergeNodes", Description = "Merge a collection of Nodes together into one node with melded properties.",
        IconBackground = Resources.URIs.MergeNodes)]
    public class MergeNodes : ModelDocumentActionBase
    {
        [ActionInput(1, "the nodes to be merged together")]
        public NodeCollection Nodes { get; set; }

        [ActionOutput(1, "the merged node")]
        public Node Node { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Node = Nodes.Merge(true);
            return true;
        }
    }
}
