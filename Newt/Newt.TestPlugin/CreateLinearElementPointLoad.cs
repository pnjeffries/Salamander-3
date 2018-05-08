using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.UI;
using Nucleus.Base;
using Nucleus.Model;
using Nucleus.Geometry;
using Nucleus.Model.Loading;

namespace Salamander.BasicTools
{
    [Action("CreateLinearElementPointLoad",
        "Create a point load applied somewhere along a linear element",
        IconBackground = Resources.URIs.LinearElementPointLoad,
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreateLinearElementPointLoad : ModelActionBase
    {
        [AutoUI(1)]
        [ActionInput(1, "the name to give to the load", Manual = false, Required = false)]
        public string Name { get; set; }

        [ActionInput(2, "the name of the load case to which the load belongs",
            SuggestionsPath = "LoadCaseSuggestions")]
        public string Case { get; set; } = "Live";

        public IList<string> LoadCaseSuggestions
        {
            get { return Model?.LoadCases?.GetNamesList(); }
        }

        [ActionInput(3, "the set of linear elements that the load is to be applied to", OneByOne = false)]
        public LinearElementCollection ApplyTo { get; set; }

        [ActionInput(4, "the value of the load, in N", Persistant = true)]
        public double Value { get; set; } = -1000;

        [ActionInput(5, "the position along the element at which the load is applied", Persistant = true)]
        public double Position { get; set; } = 0.5;

        [AutoUI(6)]
        [ActionInput(6, "whether the specified position is relative to the overall length of the element", Persistant = true, Manual = false)]
        public bool Relative { get; set; } = true;

        [AutoUI(7)]
        [ActionInput(7, "the axis in or about which the force will act", Manual = false)]
        public Direction Direction { get; set; } = Direction.Z;

        [AutoUI(7)]
        [ActionInput(7, "the coordinate system in which the force direction is defined", Manual = false)]
        public CoordinateSystemReference Axes { get; set; } = CoordinateSystemReference.Global;

        //TODO: Distribution

        [ActionOutput(1, "the created point load")]
        public LinearElementPointLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null || lCase.IsDeleted) lCase = Model.Create.LoadCase(Case, exInfo);
            LinearElementPointLoad nLoad = Model.Create.LinearElementPointLoad(lCase, exInfo);
            nLoad.Name = Name;
            nLoad.Axes = Axes;
            nLoad.Direction = Direction;
            nLoad.Value = Value;
            nLoad.Position = Position;
            nLoad.Relative = Relative;
            nLoad.AppliedTo.Set(ApplyTo);
            Load = nLoad;
            return true;
        }
    }
}
