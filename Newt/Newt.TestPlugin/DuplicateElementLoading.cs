using Nucleus.Actions;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("DuplicateElementLoading",
        "Duplicate the loading conditions applied to a particular element on others",
        IconBackground = Resources.URIs.CopyLoads)]
    public class DuplicateElementLoading : ModelActionBase
    {
        [ActionInput(1, "the elements to be modified", OneByOne = false)]
        public ElementCollection Targets { get; set; }

        [ActionInput(2, "the source element to copy the loading conditions from.")]
        public Element Source { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Source != null)
            {
                var loads = Model.Loads.AllAppliedTo(Source);
                foreach (var load in loads)
                {
                    foreach (var element in Targets)
                    {
                        if (load.CanApplyToType(element.GetType()) && !load.IsAppliedTo(element))
                        {
                            load.ApplyTo(element);
                        }
                    }
                }
            }
            return true;
        }
    }
}
