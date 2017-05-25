using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Model;
using FreeBuild.Geometry;

namespace Salamander.BasicTools
{
    [Action("CreateNodeLoad",
        "Create a point load applied directly to a set of nodes")] //TODO: Icon
    public class CreateNodeLoadAction : ModelDocumentActionBase
    {
        [ActionInput(1, "the nodes to which the load should be applied.")]
        public NodeCollection Nodes { get; set; }

        [ActionInput(2, "the name of the load case that this load belongs to")]
        public string Case { get; set; } = "Live";

        [ActionInput(3, "the direction in which the load acts.")]
        public Vector Direction { get; set; } = new Vector(0, 0, -1);

        [ActionInput(4, "the value of the load.")]
        public double Value { get; set; } = 0.0;

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null) Model.Create.LoadCase(Case, exInfo);
            // TODO!
            throw new NotImplementedException();
        }
    }
}
