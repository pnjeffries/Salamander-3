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
    [Action("CreateWall",
        Description = "Create a new vertical Panel Element.",
        IconBackground = Resources.URIs.Wall,
        IconForeground = Resources.URIs.AddIcon
        )]
    public class CreateWall : ModelActionBase
    {
        [ActionInput(1, "the line that describes the wall base")]
        public Line Line { get; set; }

        [AutoUISlider(2, Label = "Height", Max = 20)]
        [ActionInput(2, "the height of the wall, in m", Manual = false, Persistant = true)]
        public double Height { get; set; } = 4.0;

        [AutoUIComboBox("AvailableBuildUps")]
        [ActionInput(3, "the build-up of the new element", Manual = false, Persistant = true)]
        public BuildUpFamily BuildUp { get; set; }

        public BuildUpFamilyCollection AvailableBuildUps { get { return Model.Families.PanelFamilies; } }

        [ActionOutput(1, "the created element")]
        public PanelElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Line != null && Line.Length > 0 && Height != 0)
            {
                var points = new Vector[] { Line.StartPoint, Line.EndPoint,
                Line.EndPoint + new Vector(0,0,Height),  Line.StartPoint + new Vector(0,0,Height) };

                PlanarRegion pRegion = new PlanarRegion(new PolyLine(points, true));
                Element = Model.Create.PanelElement(pRegion, exInfo);
                Element.Family = BuildUp;    
            }
            return true;
        }

        public override DisplayLayer PreviewLayer(PreviewParameters parameters)
        {
            if (parameters.IsDynamic &&
                parameters.SelectionPoints != null &&
                parameters.SelectionPoints.Count == 2 &&
                parameters.SelectionPoints.ArePlanar())
            {
                ManualDisplayLayer layer = new ManualDisplayLayer();
                IMeshAvatar mesh = layer.CreateMeshAvatar();
                var points = new Vector[] { parameters.SelectionPoints[0], parameters.SelectionPoints[1],
                parameters.SelectionPoints[1] + new Vector(0,0,Height),  parameters.SelectionPoints[0] + new Vector(0,0,Height) };
                PlanarRegion pRegion = new PlanarRegion(new PolyLine(points, true));
                mesh.Builder.AddPanelPreview(pRegion, BuildUp);
                mesh.FinalizeMesh();
                layer.Add(mesh);
                return layer;
            }
            return null;
        }
    }
}

