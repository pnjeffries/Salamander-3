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
        "CreateAngleSection",
        "Create a new section property with a L-shaped profile",
        IconBackground = Resources.URIs.AngleSection,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateAngleSection : ModelActionBase
    {

        [ActionInput(1, "the name of the new section")]
        public string Name { get; set; } = "Angle Section";

        [ActionInput(2, "the depth of the section", Manual = false)]
        public double Depth { get; set; } = 0.1;

        [ActionInput(3, "the width of the section", Manual = false)]
        public double Width { get; set; } = 0.1;

        [ActionInput(4, "the thickness of the section flanges", Manual = false)]
        public double FlangeThickness { get; set; } = 0.01;

        [ActionInput(5, "the thickness of the section web", Manual = false)]
        public double WebThickness { get; set; } = 0.01;

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
            var profile = new AngleProfile(Depth, Width, FlangeThickness, WebThickness, RootRadius);
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
