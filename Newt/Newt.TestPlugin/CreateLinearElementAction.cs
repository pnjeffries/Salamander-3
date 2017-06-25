using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.UI;
using Salamander.Actions;
using Salamander.Display;

namespace Salamander.BasicTools
{
    [Action("CreateLinearElement",
            "Create a new linear element along a straight line.",
        IconBackground = Resources.URIs.LinearElement,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateLinearElementAction : ModelDocumentActionBase
    {
        [ActionInput(1, "the set-out geometry of the new element")]
        public Line Line { get; set; }

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(2, "the section of the new element", Manual = false, Persistant = true)]
        public SectionFamily Section { get; set; }

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [AutoUI(3, Label="Orientation")]
        [ActionInput(3, "the orientation angle of the new element", Manual = false, Persistant = true)]
        public Angle Orientation { get; set; } = 0;

        [ActionOutput(1, "the created element")]
        public LinearElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Line.Length > 0)
            {
                Element = Model.Create.LinearElement(Line, exInfo);
                Element.Family = Section;
                Element.Orientation = Orientation;
                Element.GenerateNodes(new NodeGenerationParameters());
                return true;
            }
            return false;
        }

        public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        {
            if (parameters.IsDynamic && parameters.CursorPoint.IsValid() && parameters.BasePoint.IsValid() && Section != null)
            {
                ManualDisplayLayer layer = new ManualDisplayLayer();
                IMeshAvatar mesh = layer.CreateMeshAvatar();
                mesh.Builder.AddSectionPreview(new Line(parameters.BasePoint, parameters.CursorPoint), Section, Orientation);
                mesh.FinalizeMesh();
                layer.Add(mesh);
                return layer;
            }
            return null;
        }
    }
}
