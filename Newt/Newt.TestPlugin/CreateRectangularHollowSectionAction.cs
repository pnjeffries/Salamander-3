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
        "CreateRectangularHollowSection",
        "Create a new section property with a rectangular hollow profile",
        IconBackground = Resources.BaseURI + "RectangularHollowSection.png",
        IconForeground = Resources.BaseURI + "AddIcon.png")]
    public class CreateRectangularHollowSectionAction : ModelActionBase
    {

        [ActionInput(1, "the name of the section")]
        public string Name { get; set; } = "Rectangular Hollow Section";

        [ActionInput(2, "the depth of the section")]
        public double Depth { get; set; } = 0.2;

        [ActionInput(3, "the width of the section")]
        public double Width { get; set; } = 0.2;

        [ActionInput(4, "the thickness of the section flanges")]
        public double FlangeThickness { get; set; } = 0.01;

        [ActionInput(5, "the thickness of the section web")]
        public double WebThickness { get; set; } = 0.01;

        [ActionOutput(1, "the output section property")]
        public SectionFamily Section { get; set; }

        [ActionOutput(2, "the output section perimeter")]
        public Curve Perimeter
        {
            get { return Section?.Profile?.Perimeter; }
        }

        [ActionOutput(3, "the output section internal void perimeter")]
        public Curve Void
        {
            get { return Section?.Profile?.Voids?.FirstOrDefault(); }
        }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var rProfile = new RectangularHollowProfile(Depth, Width, FlangeThickness, WebThickness);
            Section = Model.Create.SectionFamily(Name, exInfo);
            Section.Profile = rProfile;
            return true;
        }
    }
}
