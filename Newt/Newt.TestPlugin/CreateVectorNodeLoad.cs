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
    [Action("CreateVectorNodeLoad",
        "Create a point load described as a vector applied directly to a set of nodes",
        IconBackground = Resources.URIs.NodeLoadVector,
        IconForeground = Resources.URIs.AddIcon,
        PreviewLayerType = typeof(LoadDisplayLayer))]
    public class CreateVectorNodeLoad : ModelActionBase
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
        public Vector Force { get; set; } = new Vector(0,0,-1000);

        [ActionOutput(1, "the created node load")]
        public NodeLoad Load { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            LoadCase lCase = Model.LoadCases.FindByName(Case);
            if (lCase == null) lCase = Model.Create.LoadCase(Case, exInfo);
            NodeLoad nLoad = Model.Create.NodeLoad(lCase, exInfo);
            nLoad.Name = Name;
            nLoad.AppliedTo.Set(ApplyTo);
            nLoad.SetForce(Force);
            Load = nLoad;
            return true;
        }
    }
}

