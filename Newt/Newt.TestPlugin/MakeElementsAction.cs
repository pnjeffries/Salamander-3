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
    [Action("MakeElements",
            "Convert 'dumb' geometry into Salamander elements.")]
    public class MakeElementsAction : ModelActionBase
    {
        [ActionInput(1, "the geometry to be converted")]
        public VertexGeometryCollection Geometry { get; set; }

        [AutoUI(1, Label = "Assign Properties by Layer")]
        [ActionInput(Manual = false, Persistant = true)]
        public bool PropertiesFromLayers { get; set; } = true;

        [ActionOutput(1, "the created elements")]
        public ElementCollection Elements { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Geometry != null)
            {
                foreach (VertexGeometry shape in Geometry)
                {
                    if (shape is Curve)
                    {
                        LinearElement element = Model.Create.LinearElement((Curve)shape, exInfo);
                        if (PropertiesFromLayers && shape.Attributes != null && !string.IsNullOrWhiteSpace(shape.Attributes.LayerName))
                        {
                            string layerName = shape.Attributes.LayerName;
                            SectionFamily sP = Model.Properties.Sections.FindByName(layerName);
                            if (sP == null)
                            {
                                sP = Model.Create.SectionProperty(layerName, exInfo);
                            }
                            element.Family = sP;
                        }
                    }
                            
                }
                return true;
            }
            return false;
        }
    }
}
