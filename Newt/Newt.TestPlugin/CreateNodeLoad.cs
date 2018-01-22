using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.UI;
using Nucleus.Model;
using Nucleus.Geometry;
using Nucleus.Model.Loading;
using Nucleus.Base;

namespace Salamander.BasicTools
{
    [Action("CreateNodeLoad",
        "Create a point load applied directly to a set of nodes",
        IconBackground = Resources.URIs.NodeLoad,
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreateNodeLoad : ModelActionBase
    {
        [AutoUI(1)]
        [ActionInput(1, "the name of the node load", Manual = false, Required = false)]
        public string Name { get; set; }

        [ActionInput(2, "the name of the load case to which the load belongs",
            SuggestionsPath = "LoadCaseSuggestions")]
        public string Case { get; set; } = "Live";

        public IList<string> LoadCaseSuggestions
        {
            get { return Model?.LoadCases?.GetNamesList(); }
        }

        [ActionInput(3, "the set of nodes that the load is to be applied to")]
        public NodeCollection ApplyTo { get; set; }

        [ActionInput(4, "the value of the load, in N", Persistant = true)]
        public double Value { get; set; } = -1000;

        [AutoUI(5)]
        [ActionInput(5, "the axis in or about which the force will act", Manual = false)]
        public Direction Direction { get; set; } = Direction.Z;

        [AutoUI(6)]
        [ActionInput(6, "the coordinate system in which the force direction is defined", Manual = false)]
        public CoordinateSystemReference Axes { get; set; } = CoordinateSystemReference.Global;

        [ActionOutput(1, "the created node load")]
        public NodeLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null) lCase = Model.Create.LoadCase(Case, exInfo);
            NodeLoad nLoad = Model.Create.NodeLoad(lCase, exInfo);
            nLoad.Name = Name;
            nLoad.Axes = Axes;
            nLoad.Direction = Direction;
            nLoad.Value = Value;
            nLoad.AppliedTo.Set(ApplyTo);
            Load = nLoad;
            return true;
        }
    }
}
