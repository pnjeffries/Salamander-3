using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    [Action(
        "GetSection",
        "Retrieve an existing Section Family by name.",
        IconBackground = Resources.BaseURI + "SectionFamily.png",
        IconForeground = Resources.BaseURI + "GetIcon.png")]
    public class GetSectionAction : ModelDocumentActionBase
    {
        [ActionInput(1, "The name of the section to seach for")]
        public string Name { get; set; }

        [ActionOutput(2, "The output section")]
        public SectionFamily Section { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Section = Model.Families.FindByName(Name) as SectionFamily;
            return true;
        }
    }
}
