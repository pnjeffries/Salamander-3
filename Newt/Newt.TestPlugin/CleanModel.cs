using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("CleanModel", 
        Description = "Clean the model by removing references to deleted objects.  Those objects will then no longer be available for undeletion.",
        IconBackground = Resources.URIs.Clean)]
    public class CleanModel : ModelActionBase
    {
        [ActionInput(1, "trigger.  Set to true to clean deleted records from the active model.", Manual = false)]
        public bool Trigger { get; set; } = true;

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Model.CleanDeleted();
            return true;
        }
    }
}
