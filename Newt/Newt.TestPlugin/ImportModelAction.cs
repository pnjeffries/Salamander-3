using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("ImportModel")]
    public class ImportModelAction : ModelActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Core.Instance.ImportDocument();
            return true;
        }
    }
}
