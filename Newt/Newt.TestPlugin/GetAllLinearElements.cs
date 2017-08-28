using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Base;

namespace Salamander.BasicTools
{
    [Action("GetAllLinearElements",
        "Retrieve all linear elements in the current model",
        IconBackground = Resources.URIs.LinearElement,
        IconForeground = Resources.URIs.GetAllIcon)]
    public class GetAllLinearElements : ModelDocumentActionBase
    {
        [ActionInput(1,
           "trigger input.  This input stream will not be used directly, but exists to allow element retrieval to be automatically triggered on update of this data stream.",
           Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        [ActionOutput(1, "all linear elements in the model")]
        public LinearElementCollection Elements { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Elements = Model.Elements.LinearElements;
            Elements.RemoveDeleted();
            return true;
        }
    }
}
