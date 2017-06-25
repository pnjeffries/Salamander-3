using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("CommandList")]
    public class CommandListAction : ActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var commandList = Core.Instance.Actions.GetCommandList();
            foreach (string command in commandList)
            {
                PrintLine(command);
            }
            return true;
        }
    }
}
