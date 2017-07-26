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
            if (Core.Instance.Host.GUI.ShowOKCancelDialog("Blank Salamander Model?", 
                "Any unsaved data will be lost and Salamander model geometry will be deleted from the Rhino document.\n\nAre you sure you would like to proceed?"))
                Core.Instance.NewDocument(false);
            return true;
        }
    }
}
