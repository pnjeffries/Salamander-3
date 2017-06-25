using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Base;

namespace Salamander.BasicTools
{
    [Action("RestrainNode",
        "Set the restraint conditions on a node",
        IconBackground = Resources.URIs.NodeSupport,
        PreviewLayerType = typeof(NodeSupportDisplayLayer))]
    public class RestrainNodeAction : ActionBase
    {
        [ActionInput(1, "the Node(s) to restrain")]
        [ActionOutput(1, "the retrained Node(s)")]
        public NodeCollection Nodes { get; set; }

        [ActionInput(2, "the fixity conditions to set")]
        public Bool6D Fixity { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Nodes.Count > 0)
            {
                NodeSupport support = new NodeSupport(Fixity);
                foreach (Node node in Nodes)
                    node.SetData(support);
            }
            return true;
        }
    }
}
