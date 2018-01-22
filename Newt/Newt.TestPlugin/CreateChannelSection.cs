using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action(
        "CreateChannelSection",
        "Create a new section property with a channel profile",
        IconBackground = Resources.URIs.ChannelSection,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateChannelSection : ModelActionBase
    {

        [ActionInput(1, "the name of the new section")]
        public string Name { get; set; } = "Channel Section";

        [ActionInput(2, "the depth of the section", Manual = false)]
        public double Depth { get; set; } = 0.355;

        [ActionInput(3, "the width of the section", Manual = false)]
        public double Width { get; set; } = 0.1715;

        [ActionInput(4, "the thickness of the section flanges", Manual = false)]
        public double FlangeThickness { get; set; } = 0.0115;

        [ActionInput(5, "the thickness of the section web", Manual = false)]
        public double WebThickness { get; set; } = 0.0074;

        [ActionInput(6, "the radius of the root fillet", Manual = false)]
        public double RootRadius { get; set; } = 0;

        [ActionOutput(1, "the output section property")]
        public SectionFamily Section { get; set; }

        [ActionOutput(2, "the output section perimeter")]
        public Curve Perimeter
        {
            get { return Section?.Profile?.Perimeter; }
        }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var profile = new ChannelProfile(Depth, Width, FlangeThickness, WebThickness, RootRadius);
            Section = Model.Create.SectionFamily(Name, exInfo);
            Section.Profile = profile;
            return true;
        }

        public override bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo == null && Section != null)
            {
                // Select the new section
                Core.Instance.Selected.Select(Section);
            }
            return base.PostExecutionOperations(exInfo);
        }
    }
}
