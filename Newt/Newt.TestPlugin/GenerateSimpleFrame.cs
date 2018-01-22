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

namespace Salamander.BasicTools
{
    [Action("GenerateSimpleFrame", 
        "Generate a simple orthogonal building frame with regular column spacing",
        IconBackground = Resources.URIs.Frame,
        IconForeground = Resources.URIs.AddIcon)]
    public class GenerateSimpleFrame : ModelActionBase
    {
        [ActionInput(1, "the frame set-out location")]
        public Plane SetOut { get; set; } = new Plane();

        //[AutoUI(2)]
        [ActionInput(2, "the grid spacing in the x-direction, in m")]
        public double XSpacing { get; set; } = 6.0;

        //[AutoUI(3)]
        [ActionInput(3, "the grid spacing in the y-direction, in m")]
        public double YSpacing { get; set; } = 6.0;

        [ActionInput(4, "the maximum spacing between secondary beams, in m")]
        public double SecondarySpacing { get; set; } = 3.0;

        //[AutoUI(4)]
        [ActionInput(5, "the number of columns in the x-direction")]
        public int XCount { get; set; } = 4;

        //[AutoUI(5)]
        [ActionInput(6, "the number of columns in the y-direction")]
        public int YCount { get; set; } = 4;

        //[AutoUI(6)]
        [ActionInput(7, "the typical storey height, in m")]
        public double StoreyHeight { get; set; } = 3.00;

        //[AutoUI(7)]
        [ActionInput(8, "the height of the first storey, in m")]
        public double FirstStoreyHeight { get; set; } = 5.00;

        //[AutoUI(8)]
        [ActionInput(9, "the number of storeys in the frame")]
        public int StoreyCount { get; set; } = 3;

        [AutoUIComboBox(10, "AvailableSections")]
        [ActionInput(10, "the section to be applied to columns", Manual = false)]
        public SectionFamily ColumnSection { get; set; } = null;

        [AutoUIComboBox(11, "AvailableSections")]
        [ActionInput(11, "the section to be applied to primary beams", Manual = false)]
        public SectionFamily PrimaryBeamSection { get; set; } = null;

        [AutoUIComboBox(12, "AvailableSections")]
        [ActionInput(12, "the section to be applied to secondary beams", Manual = false)]
        public SectionFamily SecondaryBeamSection { get; set; } = null;

        public SectionFamilyCollection AvailableSections { get { return Model.Families.Sections; } }

        [ActionOutput(1, "the columns")]
        public LinearElementCollection Columns { get; set; }

        [ActionOutput(2, "the primary beams")]
        public LinearElementCollection PrimaryBeams { get; set; }

        [ActionOutput(3, "the secondary beams")]
        public LinearElementCollection SecondaryBeams { get; set; }

        

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Columns = new LinearElementCollection();
            PrimaryBeams = new LinearElementCollection();
            SecondaryBeams = new LinearElementCollection();

            int sCount = 0;
            if (SecondarySpacing > 0) sCount = (int)Math.Ceiling(XSpacing / SecondarySpacing);
            double z = FirstStoreyHeight;
            double zLast = 0;
            for (int i = 1; i <= StoreyCount; i++)
            {
                for (int j = 0; j < XCount; j++)
                {
                    for (int k = 0; k < YCount; k++)
                    {
                        Vector pt00 = SetOut.LocalToGlobal((j - 1) * XSpacing, (k - 1) * YSpacing, z);
                        Vector pt10 = SetOut.LocalToGlobal(j * XSpacing, (k - 1) * YSpacing, z);
                        Vector pt01 = SetOut.LocalToGlobal((j - 1) * XSpacing, k * YSpacing, z);
                        Vector pt11 = SetOut.LocalToGlobal(j * XSpacing, k * YSpacing, z);
                        // Column:
                        Columns.Add(Model.Create.LinearElement(pt11.WithZ(zLast), pt11, ColumnSection, exInfo));

                        if (j > 0)
                        {
                            // Primary beams:
                            PrimaryBeams.Add(Model.Create.LinearElement(pt01, pt11, PrimaryBeamSection, exInfo));
                        }
                        if (k > 0)
                        {
                            // Secondary beams:
                            SecondaryBeams.Add(Model.Create.LinearElement(pt10, pt11, SecondaryBeamSection, exInfo));

                            if (j > 0 && sCount > 1)
                            {
                                for (int s = 1; s < sCount; s++)
                                {
                                    SecondaryBeams.Add(Model.Create.LinearElement(
                                        pt00.Interpolate(pt10, ((double)s) / sCount),
                                        pt01.Interpolate(pt11, ((double)s) / sCount), SecondaryBeamSection, exInfo));
                                }
                            }
                        }
                        //TODO: Intermediate secondaries
                    }
                }
                zLast = z;
                z += StoreyHeight;
            }

            Columns.GenerateNodes(new NodeGenerationParameters());
            PrimaryBeams.GenerateNodes(new NodeGenerationParameters());
            SecondaryBeams.GenerateNodes(new NodeGenerationParameters());

            return true;
        }
    }
}
