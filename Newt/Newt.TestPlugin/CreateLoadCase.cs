using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.UI;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("CreateLoadCase", "Create a new load case",
        IconBackground = Resources.URIs.Case,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateLoadCase : ModelActionBase
    {
        [ActionOutput(1, "the name of the load case")]
        [ActionInput(1, "the name of the load case")]
        public string Name { get; set; }

        [AutoUI(2)]
        [ActionInput(2, "the nature of the load case", Manual = false)]
        public LoadCaseType Type { get; set; } = LoadCaseType.Undefined;

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Name);
            if (lCase == null || lCase.IsDeleted) lCase = Model.Create.LoadCase(Name, exInfo);
            lCase.CaseType = Type;
            return true;
        }
    }
}
