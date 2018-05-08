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
    [Action("GetAllMaterials", "Retrieve all materials in the current model",
        IconBackground = Resources.URIs.Material,
        IconForeground = Resources.URIs.GetAllIcon)]
    public class GetAllMaterials : ModelActionBase
    {
        [ActionInput(1,
           "trigger input.  This input stream will not be used directly, but exists to allow element retrieval to be automatically triggered on update of this data stream.",
           Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        [ActionOutput(1, "all panel elements in the model")]
        public MaterialCollection Materials { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Materials = new MaterialCollection(Model.Materials);
            Materials.RemoveDeleted();
            return true;
        }
    }
}
