﻿using FreeBuild.Actions;
using FreeBuild.Geometry;
using FreeBuild.Model;
using Salamander.Actions;

namespace Salamander.TestPlugin
{
    [ActionAttribute(
        "DrawLinearElement",
        "Create a new linear element between two points.")]
    public class DrawLinearElementAction : ModelActionBase
    {
        [ActionInput(1, "the set-out geometry of the new element")]
        public Line Line { get; set; }

        //[ActionInput(2, "the section property of the new element")]
        public SectionProperty Section { get; set; }

        [ActionOutput(1, "the created element")]
        public LinearElement Element { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Line.Length > 0)
            {
                Element = Model.Create.LinearElement(Line, exInfo);
                return true;
            }
            return false;
        }
    }
}
