using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Model;
using FreeBuild.UI;

namespace Salamander.BasicTools
{
    [Action("CreatePanelElement",
        Description = "Create a new Panel Element from a border curve",
        IconBackground = Resources.BaseURI + "PanelElement.png",
        IconForeground = Resources.BaseURI + "AddIcon.png"
        )]
    public class CreatePanelElement : ModelActionBase
    {
        [ActionInput(1, "the planar curve that describes the outer perimeter of the panel")]
        public Curve Perimeter { get; set; }

        [AutoUIComboBox("AvailableBuildUps")]
        [ActionInput(2, "the build-up of the new element", Manual = false, Persistant = true)]
        public BuildUpFamily BuildUp { get; set; }

        public BuildUpFamilyCollection AvailableBuildUps { get { return Model.Families.PanelFamilies; } }

        [ActionOutput(1, "the created element")]
        public PanelElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Perimeter != null && Perimeter.Plane() != null)
            {
                PlanarRegion pRegion = new PlanarRegion(Perimeter);
                Element = Model.Create.PanelElement(pRegion, exInfo);
                Element.Family = BuildUp;
                return true;
            }
            else
            { 
                throw new Exception("Input curve is not planar!");
            }
        }
    }
}
