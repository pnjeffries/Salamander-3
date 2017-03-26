using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Model;
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
        IconBackground = Resources.BaseURI + "SectionFamily.png",
        IconForeground = Resources.BaseURI + "AddIcon.png")]
    public class CreateISectionAction : ModelActionBase
    {

        [ActionInput(1, "the name of the section")]
        public string Name { get; set; } = "I Section";

        [ActionInput(2, "the depth of the section")]
        public double Depth { get; set; } = 0.355;

        [ActionInput(3, "the width of the section")]
        public double Width { get; set; } = 0.1715;

        [ActionInput(4, "the thickness of the section flanges")]
        public double FlangeThickness { get; set; } = 0.0115;

        [ActionInput(5, "the thickness of the section web")]
        public double WebThickness { get; set; } = 0.0074;

        [ActionInput(6, "the radius of the root fillet")]
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
            SymmetricIProfile iProfile = new SymmetricIProfile(Depth, Width, FlangeThickness, WebThickness, RootRadius);
            Section = Model.Create.SectionProperty(Name, exInfo);
            Section.Profile = iProfile;
            return true;
        }
    }
}
