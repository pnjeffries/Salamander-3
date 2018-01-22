using Nucleus.Actions;
using Nucleus.Base;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("GetAllPanelElements",
        "Retrieve all panel elements in the current model",
        IconBackground = Resources.URIs.PanelElement,
        IconForeground = Resources.URIs.GetAllIcon)]
    public class GetAllPanelElements : ModelActionBase
    {
        [ActionInput(1,
           "trigger input.  This input stream will not be used directly, but exists to allow element retrieval to be automatically triggered on update of this data stream.",
           Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        [ActionOutput(1, "all panel elements in the model")]
        public PanelElementCollection Elements { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Elements = Model.Elements.PanelElements;
            Elements.RemoveDeleted();
            return true;
        }
    }
}
