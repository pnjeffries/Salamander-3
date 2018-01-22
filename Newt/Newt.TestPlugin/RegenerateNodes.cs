using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("RegenerateNodes", Description = "Regenerate Element Nodes",
        IconBackground = Resources.URIs.Node, IconForeground = Resources.URIs.RegenerateIcon)]
    public class RegenerateNodes : ModelActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Model.GenerateNodes(new Nucleus.Model.NodeGenerationParameters(false));
            return true;
        }
    }
}
