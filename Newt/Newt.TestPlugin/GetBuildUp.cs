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
        "GetBuildUp",
        "Retrieve an existing Panel Build-Up by name.",
        IconBackground = Resources.BaseURI + "BuildUpFamily.png",
        IconForeground = Resources.BaseURI + "GetIcon.png")]
    public class GetPanelAction : ModelActionBase
    {
        [ActionInput(1, "The name of the build-up family to seach for")]
        public string Name { get; set; }

        [ActionOutput(2, "The output panel build-up")]
        public BuildUpFamily BuildUp { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            BuildUp = Model.Families.FindByName(Name) as BuildUpFamily;
            return true;
        }
    }
}
