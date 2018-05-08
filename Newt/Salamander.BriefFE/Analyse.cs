using Nucleus.Actions;
using Nucleus.BriefFE;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BriefFE
{
    public class Analyse : ModelActionBase
    {

        [ActionInput(1, "analysis toggle.  Set to true to run an analysis", Manual = false)]
        public bool Run { get; set; } = true;

        [ActionInput(3,
           "trigger input.  This input stream will not be used directly, but exists to allow analysis to be automatically triggered on update of this data stream.",
           Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Run)
            {
                var client = new BriefFEClient();
                client.AnalyseModel(Model);
            }
            return true;
        }
    }
}
