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
    [Action("GetElementNodes",
        "Extract the nodes from an element",
        IconBackground = Resources.BaseURI + "GetElementNodes.png")]
    public class GetElementNodesAction : ActionBase
    {
        [ActionInput(1, "the element(s) from which to extract the nodes")]
        public ElementCollection Elements { get; set; }

        [ActionOutput(1, "the element nodes")]
        public NodeCollection Nodes { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Elements != null)
            {
                Nodes = new NodeCollection();
                foreach (Element element in Elements)
                {
                    element.GenerateNodes(new NodeGenerationParameters());
                    Nodes.TryAddRange(element.Nodes);
                }
                return true;
            }
            else return false;
        }
    }
}
