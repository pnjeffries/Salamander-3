using Nucleus.Actions;
using Nucleus.Base;
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
       "TextToSection",
       "Create a new section property from a catalogue name or textual section description",
       IconBackground = Resources.URIs.TextToSection)]
    public class TextToSectionAction : ModelActionBase
    {
        [ActionInput(1, "the name of the section")]
        public string Name { get; set; } = "Section";

        [ActionInput(2, "the catalogue description of the section")]
        public string Description { get; set; } 

        [ActionOutput(1, "the output section property")]
        public SectionFamily Section { get; set; }

        [ActionOutput(2, "the output section perimeter")]
        public Curve Perimeter
        {
            get { return Section?.Profile?.Perimeter; }
        }

        [ActionOutput(3, "the output section internal void perimeter (if any)")]
        public Curve Void
        {
            get { return Section?.Profile?.Voids?.FirstOrDefault(); }
        }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            SectionProfile profile = SectionProfile.FromDescription(Description, Core.Instance.SectionLibrary);
            Section = Model.Create.SectionFamily(Name, exInfo);
            Section.Profile = profile;
            return true;
        }
    }
}
