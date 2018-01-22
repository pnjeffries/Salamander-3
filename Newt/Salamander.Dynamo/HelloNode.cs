using Dynamo.Graph.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoCore.AST.AssociativeAST;

namespace Salamander.Dynamo
{
    public class HelloNode : NodeModel
    {
        public HelloNode()
        {
            InPortData.Add(new PortData("Name", "Enter your name"));
            OutPortData.Add(new PortData("Message", "Message for you"));

            RegisterAllPorts();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            return base.BuildOutputAst(inputAstNodes);
        }
    }
}
