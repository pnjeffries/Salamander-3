using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Geometry;
using Nucleus.UI;
using Nucleus.Model;
using Nucleus.Extensions;

namespace Salamander.BasicTools
{
    [Action("GeneratePrattTruss",
        "Generate a 2D Pratt Truss between two curves",
        IconBackground = Resources.URIs.PrattTruss,
        IconForeground = Resources.URIs.AddIcon)]
    public class GeneratePrattTruss : ModelDocumentActionBase
    {
        [ActionInput(1, "the curve describing the set-out of the top chord of the truss")]
        public Curve TopChord { get; set; }

        [ActionInput(2, "the curve describing the set-out of the bottom chord of the truss")]
        public Curve BottomChord { get; set; }

        [AutoUI()]
        [ActionInput(3, "the maximum distance along the chords between nodes (in m)")]
        public double NodeSpacing { get; set; } = 0;

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(4, "the section of the chords", Manual = false, Persistant = true)]
        public SectionFamily ChordSection { get; set; }

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(5, "the section of the posts", Manual = false, Persistant = true)]
        public SectionFamily PostSection { get; set; }

        [AutoUIComboBox("AvailableSections")]
        [ActionInput(5, "the section of the bracing", Manual = false, Persistant = true)]
        public SectionFamily BracingSection { get; set; }

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [ActionOutput(1, "the elements in the top chord")]
        public LinearElementCollection TopChordElements { get; set; }

        [ActionOutput(2, "the elements in the bottom chord")]
        public LinearElementCollection BottomChordElements { get; set; }

        [ActionOutput(3, "the post elements")]
        public LinearElementCollection PostElements { get; set; }

        [ActionOutput(4, "the bracing elements")]
        public LinearElementCollection BracingElements { get; set; }

        [ActionOutput(5, "the nodes in the top chord")]
        public NodeCollection TopChordNodes { get; set; }

        [ActionOutput(6, "the nodes in the bottom chord")]
        public NodeCollection BottomChordNodes { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            TopChordElements = new LinearElementCollection();
            BottomChordElements = new LinearElementCollection();
            PostElements = new LinearElementCollection();
            BracingElements = new LinearElementCollection();

            if (TopChord != null && BottomChord != null)
            {
                int divisions;
                double maxLength = Math.Max(TopChord.Length, BottomChord.Length);
                if (NodeSpacing <= 0 || maxLength / NodeSpacing > 1000)
                {
                    //Auto-determine appropriate node spacing
                    Vector[] samplePointsT = TopChord.Divide(4);
                    Vector[] samplePointsB = BottomChord.Divide(4);
                    double tDist = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        tDist += samplePointsB[i].DistanceTo(samplePointsT[i]);
                    }
                    tDist /= 5;
                    divisions = (int)(maxLength / tDist).Round(2);
                }
                else divisions = (int)Math.Ceiling(maxLength / NodeSpacing);

                if (divisions > 0)
                {
                    Vector[] topPts = TopChord.Divide(divisions);
                    Vector[] bottomPts = BottomChord.Divide(divisions);

                    //foreach (Vector v in topPts) TopChordNodes.Add(Model.Create.Node(v, 0, exInfo));
                    //foreach (Vector v in bottomPts) BottomChordNodes.Add(Model.Create.Node(v, 0, exInfo));

                    for (int i = 0; i < topPts.Length - 1; i++)
                    {
                        //Node tNode0 = TopChordNodes[i];
                        //Node tNode1 = TopChordNodes[i + 1];
                        //Node bNode0 = BottomChordNodes[i];
                        //Node bNode1 = BottomChordNodes[i + 1];

                        Vector tNode0 = topPts[i];
                        Vector tNode1 = topPts[i + 1];
                        Vector bNode0 = bottomPts[i];
                        Vector bNode1 = bottomPts[i + 1];

                        TopChordElements.Add(Model.Create.LinearElement(tNode0, tNode1, ChordSection, exInfo));
                        BottomChordElements.Add(Model.Create.LinearElement(bNode0, bNode1, ChordSection, exInfo));
                        if (i > 0) PostElements.Add(Model.Create.LinearElement(tNode0, bNode0, PostSection, exInfo));
                        if (i < topPts.Length / 2)
                            BracingElements.Add(Model.Create.LinearElement(tNode0, bNode1, BracingSection, exInfo));
                        else
                            BracingElements.Add(Model.Create.LinearElement(bNode0, tNode1, BracingSection, exInfo));

                    }

                    TopChordElements.GenerateNodes(new NodeGenerationParameters());
                    BottomChordElements.GenerateNodes(new NodeGenerationParameters());
                    PostElements.GenerateNodes(new NodeGenerationParameters());
                    BracingElements.GenerateNodes(new NodeGenerationParameters());

                    TopChordNodes = TopChordElements.GetNodes();
                    BottomChordNodes = BottomChordElements.GetNodes();

                    TopChordNodes.ClearAttachedData();
                    BottomChordNodes.ClearAttachedData();
                }
            }

            return true;
        }
    }
}
