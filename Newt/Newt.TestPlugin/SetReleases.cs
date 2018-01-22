using Nucleus.Base;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;

namespace Salamander.BasicTools
{
    [Action("SetReleases", Description = "Set the end releases of a linear element",
        IconBackground = Resources.URIs.ElementReleases,
        PreviewLayerType = typeof(ElementReleasesDisplayLayer))]
    public class SetReleases : ModelActionBase
    {
        [ActionInput(1, "the element(s) to release")]
        [ActionOutput(1, "the released elements")]
        public LinearElementCollection Elements { get; set; }

        [ActionInput(2, "the directions to release at the start of the element")]
        public Bool6D Start { get; set; }

        [ActionInput(3, "the directions to release at the start of the element")]
        public Bool6D End { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            foreach (LinearElement lEl in Elements)
            {
                var sV = lEl.Start;
                sV.Releases = Start;
                var eV = lEl.End;
                eV.Releases = End;
            }
            return true;
        }
    }
}
