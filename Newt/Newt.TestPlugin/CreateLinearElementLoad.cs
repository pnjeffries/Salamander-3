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
    [Action("CreateLinearElementLoad", 
        "Create a line load applied along a linear element",
        IconBackground = Resources.URIs.LinearElementUDL, 
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreateLinearElementLoad : ModelActionBase
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

        [ActionInput(4, "the value of the load, in N/m", Persistant = true)]
        public double Value { get; set; } = -1000;

        [AutoUI(5)]
        [ActionInput(5, "the axis in or about which the force will act", Manual = false)]
        public Direction Direction { get; set; } = Direction.Z;

        [AutoUI(6)]
        [ActionInput(6, "the coordinate system in which the force direction is defined", Manual = false)]
        public CoordinateSystemReference Axes { get; set; } = CoordinateSystemReference.Global;

        //TODO: Distribution

        [ActionOutput(1, "the created node load")]
        public LinearElementLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null || lCase.IsDeleted) lCase = Model.Create.LoadCase(Case, exInfo);
            LinearElementLoad nLoad = Model.Create.LinearElementLoad(lCase, exInfo);
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
