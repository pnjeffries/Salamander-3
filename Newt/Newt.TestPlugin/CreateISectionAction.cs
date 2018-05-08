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
        "CreateISection",
        "Create a new section property with a symmetric I-shaped profile",
        IconBackground = Resources.URIs.SectionFamily,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateISectionAction : ModelActionBase
    {

        [ActionInput(1, "the name of the new section")]
        public string Name { get; set; } = "I Section";

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

        [ActionInput(7, "the material of the section", Required = false, Manual = false)]
        public Material Material { get; set; }

        [ActionOutput(1, "the output section property")]
        public SectionFamily Section { get; set; }

        [ActionOutput(2, "the output section perimeter")]
        public Curve Perimeter
        {
            get { return Section?.Profile?.Perimeter; }
        }

        public override bool Execute(ExecutionInfo exInfo = null)
        { 
            SymmetricIProfile iProfile = new SymmetricIProfile(Depth, Width, FlangeThickness, WebThickness, RootRadius);
            iProfile.Material = Material;
            Section = Model.Create.SectionFamily(Name, exInfo);
            Section.Profile = iProfile;
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
