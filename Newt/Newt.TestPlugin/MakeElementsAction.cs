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
            "Convert 'dumb' geometry into Salamander elements.",
        IconBackground = Resources.URIs.Convert)]
    public class MakeElementsAction : ModelDocumentActionBase
    {
        [ActionInput(1, "the geometry to be converted")]
        public VertexGeometryCollection Geometry { get; set; }

        [AutoUI(1, Label = "Assign Families by Layer")]
        [ActionInput(Manual = false, Persistant = true)]
        public bool FamiliesFromLayers { get; set; } = true;

        [ActionOutput(1, "the created elements")]
        public ElementCollection Elements { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Elements = new ElementCollection();

            if (Geometry != null)
            {
                // Convert each geometry item:
                foreach (VertexGeometry shape in Geometry)
                {
                    if (shape is Curve) // Convert to linear element
                    {
                        LinearElement element = Model.Create.LinearElement((Curve)shape, exInfo);
                        if (FamiliesFromLayers && shape.Attributes != null && !string.IsNullOrWhiteSpace(shape.Attributes.LayerName))
                        {
                            string layerName = shape.Attributes.LayerName;
                            SectionFamily sF = Model.Families.Sections.FindByName(layerName);
                            if (sF == null)
                            {
                                sF = Model.Create.SectionFamily(layerName, exInfo);
                            }
                            element.Family = sF;
                        }
                        Elements.Add(element);
                    }
                    else if (shape is Surface) //Reminder: Meshes are also surfaces!
                    {
                        PanelElement element = Model.Create.PanelElement((Surface)shape, exInfo);
                        if (FamiliesFromLayers && shape.Attributes != null && !string.IsNullOrWhiteSpace(shape.Attributes.LayerName))
                        {
                            string layerName = shape.Attributes.LayerName;
                            BuildUpFamily fF = Model.Families.PanelFamilies.FindByName(layerName);
                            if (fF == null)
                            {
                                fF = Model.Create.BuildUpFamily(layerName, exInfo);
                            }
                            element.Family = fF;
                        }
                        Elements.Add(element);
                    }                
                }
                Elements.GenerateNodes(new NodeGenerationParameters());
                return true;
            }
            return false;
        }
    }
}
