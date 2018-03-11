using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.UI;
using Nucleus.Meshing;
using Salamander.Actions;
using Salamander.Display;

namespace Salamander.BasicTools
{
    [Action("PreviewLinearElement",
            "Preview the geometry of linear element as a mesh without creating it in either the main or backing model.",
        IconBackground = Resources.URIs.LinearElement,
        IconForeground = Resources.URIs.PreviewIcon)]
    public class PreviewLinearElement : ModelActionBase
    {
        [ActionInput(1, "the set-out geometry of the new element")]
        public Line Line { get; set; }

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(2, "the section of the new element", Manual = false, Persistant = true)]
        public SectionFamily Section { get; set; }

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [AutoUI(3, Label = "Orientation")]
        [ActionInput(3, "the orientation angle of the new element", Manual = false, Persistant = true)]
        public Angle Orientation { get; set; } = 0;

        //[ActionOutput(1, "the created element")]
        public LinearElement Element { get; set; }

        [ActionOutput(1, "the element mesh geometry preview")]
        public Mesh Mesh { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Line.Length > 0)
            {
                Element = new LinearElement(Line);
                Element.Family = Section;
                Element.Orientation = Orientation;
                MeshBuilder builder = new MeshBuilder();
                builder.AddSectionPreview(Element);
                Mesh = builder.Mesh;
                return true;
            }
            return false;
        }

        //public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        //{
        //    if (parameters.IsDynamic &&
        //        parameters.SelectionPoints != null &&
        //        parameters.SelectionPoints.Count >= 2 &&
        //        Section != null)
        //    {
        //        ManualDisplayLayer layer = new ManualDisplayLayer();
        //        IMeshAvatar mesh = layer.CreateMeshAvatar();
        //        mesh.Builder.AddSectionPreview(
        //            new Line(parameters.SelectionPoints[0], parameters.SelectionPoints[1])
        //            , Section, Orientation);
        //        mesh.FinalizeMesh();
        //        layer.Add(mesh);
        //        return layer;
        //    }
        //    return null;
        //}
    }
}
