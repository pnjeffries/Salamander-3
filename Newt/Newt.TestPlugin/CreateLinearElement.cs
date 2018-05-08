using Nucleus.Actions;
using Nucleus.Base;
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
    public class CreateLinearElement : ModelActionBase
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

        [AutoUI(4, Label="Start Releases")]
        [ActionInput(4, "the releases at the start of the element", Manual = false, Parametric = false, Persistant = true)]
        public Bool6D StartReleases { get; set; } = Bool6D.False;

        [AutoUI(5, Label = "End Releases")]
        [ActionInput(4, "the releases at the end of the element", Manual = false, Parametric = false, Persistant = true)]
        public Bool6D EndReleases { get; set; } = Bool6D.False;

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
                if (!StartReleases.AllFalse) Element.Start.Releases = StartReleases;
                if (!EndReleases.AllFalse) Element.End.Releases = EndReleases;
                return true;
            }
            return false;
        }

        public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        {
            if (parameters.IsDynamic && 
                parameters.SelectionPoints != null && 
                parameters.SelectionPoints.Count >= 2 && 
                Section != null)
            {
                ManualDisplayLayer layer = new ManualDisplayLayer();
                IMeshAvatar mesh = layer.CreateMeshAvatar();
                mesh.Builder.AddSectionPreview(
                    new Line(parameters.SelectionPoints[0], parameters.SelectionPoints[1])
                    , Section, Orientation);
                mesh.FinalizeMesh();
                layer.Add(mesh);
                return layer;
            }
            return null;
        }
    }
}
