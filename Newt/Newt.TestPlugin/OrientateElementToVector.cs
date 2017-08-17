using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Geometry;

namespace Salamander.BasicTools
{
    [Action("OrientateElementToVector", 
        Description = "Set element orientation such that the relevant local axis of the element (Z-axis for linear elements, X-axis for panels) will align as closely as possible with a given vector.",
        IconBackground = Resources.URIs.ElementOrientation)]
    public class OrientateElementToVector : ActionBase
    {
        [ActionInput(1, "the element(s) to orientate")]
        [ActionOutput(1, "the re-orientated elements")]
        public ElementCollection Elements { get; set; }

        [ActionInput(2, "the direction to orientate the element's local Z-axis towards")]
        public Vector Vector { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            foreach (var element in Elements)
            {
                element.OrientateToVector(Vector);
            }
            return true;
        }
    }
}
