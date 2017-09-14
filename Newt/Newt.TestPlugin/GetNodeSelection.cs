using Nucleus.Actions;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("GetNodeSelection", "Get the selection of nodes in a saved set",
        IconBackground = Resources.URIs.NodeSet, IconForeground = Resources.URIs.GetIcon)]
    public class GetNodeSelection : ModelDocumentActionBase
    {
        [ActionInput(1, "the name of the node set to retrieve")]
        public string Name { get; set; }

        [ActionOutput(1, "the nodes in the set")]
        public NodeCollection Nodes { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var set = Model.Sets.FindByName<NodeSet>(Name);
            if (set == null) PrintLine("No node set found named '" + Name + "'!");
            else Nodes = set.Items;
            return true;
        }

        public override bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo == null && Nodes != null) Core.Instance.Select(Nodes);
            return true;
        }
    }
}
