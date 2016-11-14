using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Model;

namespace Salamander.TestPlugin
{
    [Action(
        "CreateRectangularSection",
        "Create a new section property with a rectangular profile")]
    public class CreateRectangularSectionAction : ModelActionBase
    {
      
        [ActionInput(1,"the depth of the section")]
        public double Depth { get; set; }

        [ActionInput(2,"the width of the section")]
        public double Width { get; set; }

        [ActionOutput(3, "the output section property")]
        public SectionProperty Section { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            RectangularProfile rProfile = new RectangularProfile(Depth, Width);
            Section = Model.Create.SectionProperty(rProfile, exInfo);
            return true;
        }
    }
}
