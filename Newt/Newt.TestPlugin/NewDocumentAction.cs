using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("NewDocument",
        Description = "Blank the current Salamander model and start a new one.",
        IconBackground = Resources.URIs.New)]
    public class NewDocumentAction : ActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Core.Instance.NewDocument(false);
            return true;
        }
    }
}
