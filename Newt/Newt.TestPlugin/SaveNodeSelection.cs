using Nucleus.Base;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("SaveNodeSelection", "Save a selection of nodes as a named node set",
        IconBackground = Resources.URIs.NodeSet, IconForeground = Resources.URIs.AddIcon)]
    public class SaveNodeSelection : ModelDocumentActionBase
    {
        [ActionInput(1, "the node selection to be saved as a Set", OneByOne = false)]
        public NodeCollection Nodes { get; set; }

        [ActionInput(2, "the name of the set to be created/overwritten",
            SuggestionsPath = "NameSuggestions")]
        public string Name { get; set; }

        public IList<string> NameSuggestions
        {
            get { return Model?.Sets?.NodeSets.GetNamesList(); }
        }

        public override bool Execute(Nucleus.Actions.ExecutionInfo exInfo = null)
        {
            NodeSet set = Model.Sets.FindOrCreate<NodeSet>(Name);
            set.Set(Nodes);
            return true;
        }
    }
}
