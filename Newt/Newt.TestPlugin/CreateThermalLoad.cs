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
    [Action("CreateThermalLoad",
        "Create a thermal load applied to a set of elements",
        IconBackground = Resources.URIs.Temperature,
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreateThermalLoad : ModelActionBase
    {
        [AutoUI(1)]
        [ActionInput(1, "the name to give to the load", Manual = false, Required = false)]
        public string Name { get; set; }

        [ActionInput(2, "the name of the load case to which the load belongs",
            SuggestionsPath = "LoadCaseSuggestions")]
        public string Case { get; set; } = "Temperature";

        public IList<string> LoadCaseSuggestions
        {
            get { return Model?.LoadCases?.GetNamesList(); }
        }

        [ActionInput(3, "the set of elements that the load is to be applied to " +
            "(Leave null to apply to all)",
            Required = false)]
        public ElementCollection ApplyTo { get; set; } = null;

        [ActionInput(4, "the value of the temperature change, in °C", Persistant = true)]
        public double Value { get; set; }

        /*
        [AutoUI(5)]
        [ActionInput(5, "the axis in or about which the force will act", Manual = false)]
        public Direction Direction { get; set; } = Direction.Z;

        [AutoUI(6)]
        [ActionInput(6, "the coordinate system in which the force direction is defined", Manual = false)]
        public CoordinateSystemReference Axes { get; set; } = CoordinateSystemReference.Global;
        */

        [ActionOutput(1, "the created thermal load")]
        public ThermalLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null) lCase = Model.Create.LoadCase(Case, LoadCaseType.Thermal, exInfo);
            ThermalLoad nLoad = Model.Create.ThermalLoad(lCase, exInfo);
            nLoad.Name = Name;
            nLoad.Value = Value;
            if (ApplyTo == null)
            {
                nLoad.AppliedTo.Clear();
                nLoad.AppliedTo.All = true;
            }
            else nLoad.AppliedTo.Set(ApplyTo);
            Load = nLoad;
            return true;
        }
    }
}
