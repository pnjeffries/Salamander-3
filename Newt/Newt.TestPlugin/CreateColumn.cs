using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.UI;
using Salamander.Actions;
using Salamander.Display;
using System.Linq;

namespace Salamander.BasicTools
{
    [Action("CreateColumn",
            "Create a new vertical linear element.",
        IconBackground = Resources.URIs.Column,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateColumn : ModelActionBase
    {
        [ActionInput(1, "the position of the base of the column")]
        public Vector Position { get; set; }

        [AutoUISlider(2, Label = "Height", Max = 20)]
        [ActionInput(2, "the height of the column", Manual = false, Persistant = true)]
        public double Height { get; set; } = 4.0;

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(3, "the section of the new element", Manual = false, Persistant = true)]
        public SectionFamily Section { get; set; }

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [AutoUI(4, Label = "Orientation")]
        [ActionInput(4, "the orientation angle of the new element", Manual = false, Persistant = true)]
        public Angle Orientation { get; set; } = 0;

        [ActionOutput(1, "the created element")]
        public LinearElement Element { get; set; }

        [ActionOutput(2, "the top of the created column")]
        public Vector TopPoint { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Position.IsValid() && Height > 0)
            {
                TopPoint = Position + new Vector(0, 0, Height);
                Element = Model.Create.LinearElement(
                    new Line(Position,TopPoint), exInfo);
                Element.Family = Section;
                Element.Orientation = Orientation;
                Element.GenerateNodes(new NodeGenerationParameters());
                return true;
            }
            return false;
        }

        public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        {
            if (parameters.IsDynamic && parameters.SelectionPoints != null && parameters.SelectionPoints.Count > 0)
            {
                Vector cursorPt = parameters.SelectionPoints.Last();
                ManualDisplayLayer layer = new ManualDisplayLayer();
                var cL = new Line(cursorPt, cursorPt + new Vector(0, 0, Height));
                layer.Add(layer.CreateCurveAvatar(cL));
                if (Section != null)
                {
                    IMeshAvatar mesh = layer.CreateMeshAvatar();
                    mesh.Builder.AddSectionPreview(cL, Section, Orientation);
                    mesh.FinalizeMesh();
                    layer.Add(mesh);
                }
                return layer;
            }
            return null;
        }
    }
}
