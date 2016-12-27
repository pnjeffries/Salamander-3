using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;

namespace Salamander.BasicTools
{
    [Action("RegenerateNodes")]
    public class RegenerateNodes : ModelActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Model.RegenerateNodes(new FreeBuild.Model.NodeGenerationParameters());
            return true;
        }
    }
}
