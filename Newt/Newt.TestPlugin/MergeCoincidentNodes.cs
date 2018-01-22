using Nucleus.Actions;
using Nucleus.DDTree;
using Nucleus.Model;
using Nucleus.UI;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("MergeCoincidentNodes", Description = "Merge together any nodes which are within tolerance of each other.",
        IconBackground = Resources.URIs.MergeNodes)]
    public class MergeCoincidentNodes : ModelActionBase
    {
        [ActionInput(1, "the nodes to tested for coincidence and (potentially) to be merged together")]
        public NodeCollection Nodes { get; set; }

        [AutoUI()]
        [ActionInput(2, "the tolerance distance within which nodes are to be merged", Manual = false, Persistant = true)]
        public double Tolerance { get; set; } = 0.001;

        [AutoUI(Label = "Average Positions")]
        [ActionInput(3, "whether the positions of merged node clusters should be averaged.  If not, the original position of the node the others are merged into will be kept.", Manual = false, Persistant = true)]
        public bool AveragePositions { get; set; } = true;

        [ActionOutput(1, "the post-merging nodes")]
        public NodeCollection MergedNodes { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            MergedNodes = new NodeCollection();
            NodeDDTree tree = new NodeDDTree(Nodes);
            var nodeSets = tree.CoincidentNodes(Nodes, Tolerance);
            foreach (var nodeSet in nodeSets)
            {
                if (nodeSet.Count > 1)
                    MergedNodes.Add(nodeSet.Merge());
                else
                    MergedNodes.Add(nodeSet[0]);
            }
            return true;
        }
    }
}
