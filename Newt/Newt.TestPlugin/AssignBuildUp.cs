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
    [Action("AssignBuildUp", "Assign panel element build-up families",
        Resources.URIs.BuildUpFamily, Resources.URIs.AssignIcon)]
    public class AssignBuildUp : ActionBase
    {
        [ActionInput(1, "the element(s) to assign a build-up family to")]
        [ActionOutput(2, "the modified element(s)")]
        public PanelElementCollection Elements { get; set; }

        [ActionInput(2, "the section to be assigned to the element")]
        public BuildUpFamily BuildUp { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            foreach (var element in Elements)
            {
                element.Family = BuildUp;
            }
            return true;
        }
    }
}
