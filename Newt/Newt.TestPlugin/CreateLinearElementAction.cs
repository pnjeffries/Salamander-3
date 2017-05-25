using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Model;
using FreeBuild.UI;
using Salamander.Actions;
using Salamander.Display;

namespace Salamander.BasicTools
{
    [Action("DrawLinearElement",
            "Create a new linear element along a straight line.",
        IconBackground = Resources.BaseURI + "LinearElement.png",
        IconForeground = Resources.BaseURI + "AddIcon.png")]
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
