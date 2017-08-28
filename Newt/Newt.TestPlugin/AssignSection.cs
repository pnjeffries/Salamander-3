using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;

namespace Salamander.BasicTools
{
    [Action("AssignSection", "Assign element sections",
        Resources.URIs.SectionFamily, Resources.URIs.AssignIcon)]
    public class AssignSection : ActionBase
    {
        [ActionInput(1, "the element(s) to assign a section to")]
        [ActionOutput(2, "the modified element(s)")]
        public LinearElementCollection Elements { get; set; }

        [ActionInput(2, "the section to be assigned to the element")]
        public SectionFamily Section { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            foreach (var element in Elements)
            {
                element.Family = Section;
            }
            return true;
        }
    }
}
