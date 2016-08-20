using FreeBuild.Geometry;
using FreeBuild.Model;
using Newt.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.TestPlugin
{
    [ActionAttribute("DrawLinearElement",
        "Create a new 1D Element between two points, creating new nodes as necessary.")]
    public class DrawLinearElementAction : ModelActionBase
    {
        [ActionInput(1, "the set-out geometry of the new element")]
        public Line Line { get; set; }

        [ActionOutput(1, "the created element")]
        public LinearElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Line.Length > 0)
            {
                Element = new LinearElement(Line);
                Model.Add(Element);
                return true;
            }
            return false;
        }
    }
}
