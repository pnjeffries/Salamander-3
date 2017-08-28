using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Robot;

namespace Salamander.RobotIOPlugin
{
    [Action("RobotUpdateOptions", "Specify Robot update options",
        Resources.URIs.Robot, Resources.URIs.CheckIcon)]
    public class RobotUpdateOptionsAction : ActionBase
    {
        [ActionInput(1, "Update nodes?")]
        public bool Nodes { get; set; } = true;

        [ActionInput(2, "Update linear elements (bars)?")]
        public bool LinearElements { get; set; } = true;

        [ActionInput(3, "Update panel elements?")]
        public bool PanelElements { get; set; } = true;

        [ActionInput(4, "Update section and thickness properties")]
        public bool Families { get; set; } = true;

        [ActionInput(5, "Delete objects in Robot that have been deleted in Salamander?")]
        public bool Delete { get; set; } = true;

        [ActionOutput(1, "the constructed options")]
        public RobotConversionOptions Options { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Options = new RobotConversionOptions();
            Options.Nodes = Nodes;
            Options.LinearElements = LinearElements;
            Options.PanelElements = PanelElements;
            Options.Families = Families;
            Options.DeleteObjects = Delete;
            return true;
        }
    }
}
