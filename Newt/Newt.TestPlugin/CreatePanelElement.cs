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
using Salamander.Display;

namespace Salamander.BasicTools
{
    [Action("CreatePanelElement",
        Description = "Create a new Panel Element from a set of edge vertices.",
        IconBackground = Resources.URIs.PanelElement,
        IconForeground = Resources.URIs.AddIcon
        )]
    public class CreatePanelElement : ModelActionBase
    {
        [ActionInput(1, "the polyline points that describe the outside edge of the panel")]
        public IList<Vector> Points { get; set; }

        [AutoUIComboBox("AvailableBuildUps")]
        [ActionInput(2, "the build-up of the new element", Manual = false, Persistant = true)]
        public BuildUpFamily BuildUp { get; set; }

        public BuildUpFamilyCollection AvailableBuildUps { get { return Model.Families.PanelFamilies; } }

        [ActionOutput(1, "the created element")]
        public PanelElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Points != null && Points.Count > 2)
            {
                if (!Points.ArePlanar()) throw new Exception("Points do not lie on the same plane!");
                // TODO: Check for re-entrancy!
                PlanarRegion pRegion = new PlanarRegion(new PolyLine(Points, true));
                Element = Model.Create.PanelElement(pRegion, exInfo);
                Element.Family = BuildUp;
                return true;
            }
            else
            { 
                throw new Exception("Insufficient points to define a panel!");
            }
        }

        public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        {
            if (parameters.IsDynamic &&
                parameters.SelectionPoints != null &&
                parameters.SelectionPoints.Count > 2 &&
                parameters.SelectionPoints.ArePlanar())
            {
                ManualDisplayLayer layer = new ManualDisplayLayer();
                IMeshAvatar mesh = layer.CreateMeshAvatar();
                PlanarRegion pRegion = new PlanarRegion(new PolyLine(parameters.SelectionPoints, true));
                mesh.Builder.AddPanelPreview(pRegion, BuildUp);
                mesh.FinalizeMesh();
                layer.Add(mesh);
                return layer;
            }
            return null;
        }
    }
}
