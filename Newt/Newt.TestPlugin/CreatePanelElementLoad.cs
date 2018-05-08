using Nucleus.Actions;
using Nucleus.Base;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Model.Loading;
using Nucleus.UI;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("CreatePanelLoad",
        "Create an area load applied accross a panel element",
        IconBackground = Resources.URIs.PanelElementLoad,
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreatePanelElementLoad : ModelActionBase
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

        [ActionInput(3, "the set of panel elements that the load is to be applied to", OneByOne = false)]
        public PanelElementCollection ApplyTo { get; set; }

        [ActionInput(4, "the value of the load, in N/m²", Persistant = true)]
        public double Value { get; set; } = -1000;

        [AutoUI(5)]
        [ActionInput(5, "the axis in or about which the force will act", Manual = false)]
        public Direction Direction { get; set; } = Direction.Z;

        [AutoUI(6)]
        [ActionInput(6, "the coordinate system in which the force direction is defined", Manual = false)]
        public CoordinateSystemReference Axes { get; set; } = CoordinateSystemReference.Global;

        //TODO: Distribution?

        [ActionOutput(1, "the created panel load")]
        public PanelLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null) lCase = Model.Create.LoadCase(Case, exInfo);
            PanelLoad nLoad = Model.Create.PanelLoad(lCase, exInfo);
            nLoad.Name = Name;
            nLoad.Axes = Axes;
            nLoad.Direction = Direction;
            nLoad.Value = Value;
            nLoad.AppliedTo.Set(ApplyTo.ToElementCollection());
            Load = nLoad;
            return true;
        }
    }
}
