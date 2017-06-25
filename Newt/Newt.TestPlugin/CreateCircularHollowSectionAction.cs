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
       "CreateCircularHollowSection",
       "Create a new section property with a circular hollow profile",
       IconBackground = Resources.URIs.CircularHollowSection,
       IconForeground = Resources.URIs.AddIcon)]
    public class CreateCircularHollowSectionAction : ModelDocumentActionBase
    {
        [ActionInput(1, "the name of the section")]
        public string Name { get; set; } = "Circular Hollow Section";

        [ActionInput(2, "the depth of the section")]
        public double Diameter { get; set; } = 0.2191;

        [ActionInput(3, "the wall thickness of the section")]
        public double WallThickness { get; set; } = 0.01;

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
            var profile = new CircularHollowProfile(Diameter, WallThickness);
            Section = Model.Create.SectionFamily(Name, exInfo);
            Section.Profile = profile;
            return true;
        }
    }
}
