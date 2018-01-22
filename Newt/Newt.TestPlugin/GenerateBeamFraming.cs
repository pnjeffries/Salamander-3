using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.UI;

namespace Salamander.BasicTools
{
    [Action("GenerateBeamFraming",
        "Automatically generate a 'sensible' quadrangular beam framing arrangement between " +
        "an unordered set of support positions in plan.  Work in progress!",
        IconBackground = Resources.URIs.BeamFraming,
        IconForeground = Resources.URIs.AddIcon)]
    public class GenerateBeamFraming : ModelActionBase
    {
        [ActionInput(1, "the support positions (for e.g. the column locations in plan)", OneByOne = false)]
        public IList<Vector> SupportPoints { get; set; }

        [ActionInput(2, "the planar curve that describes the outer perimeter of the region to be framed.  Optional", Required = false)]
        public Curve Perimeter { get; set; }

        [AutoUIComboBox(3, "AvailableSections")]
        [ActionInput(3, "the section to be applied to primary beams", Manual = false)]
        public SectionFamily BeamSection { get; set; }

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [ActionOutput(1, "the generated beams")]
        public LinearElementCollection Beams { get; set; }

        [ActionOutput(2, "the boundary curves of the resultant panels")]
        public CurveCollection PanelBoundaries { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Beams = new LinearElementCollection();
            VertexCollection verts = new VertexCollection(SupportPoints);
            MeshFaceCollection faces = Mesh.DelaunayTriangulationXY(verts);
            if (Perimeter != null) faces.CullOutsideXY(Perimeter);
            faces.Quadrangulate();
            IList<MeshEdge> edges = faces.ExtractUniqueEdges();
            foreach (MeshEdge mE in edges)
            {
                    LinearElement lEl = Model.Create.LinearElement(mE.ToLine(), exInfo);
                    lEl.Family = BeamSection;
                    Beams.Add(lEl);
            }
            PanelBoundaries = faces.ExtractFaceBoundaries();
            return true;
        }
    }
}
