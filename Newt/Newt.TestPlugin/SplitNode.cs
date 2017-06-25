using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Geometry;

namespace Salamander.BasicTools
{
    [Action("SplitNode", "Split a node into several nodes, one for each connected element.",
        IconBackground = Resources.URIs.SplitNode)]
    public class SplitNode : ModelDocumentActionBase
    {
        [ActionInput(1, "the node to be split")]
        public Node Node { get; set; }

        [ActionOutput(1, "the resultant nodes, including the original")]
        public NodeCollection Nodes { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Node?.Vertices != null && Node.Vertices.Count > 1)
            {
                Nodes = new NodeCollection();
                Nodes.Add(Node);
                VertexCollection vertices = new VertexCollection(Node.Vertices);
                for (int i = 1; i < vertices.Count; i++)
                {
                    Node newNode = Model.Create.CopyOf(Node, exInfo);
                    Vertex v = vertices[i];
                    if (v.Node == Node) v.Node = newNode;
                    Nodes.Add(newNode);
                }
            }
            return true;
        }
    }
}
