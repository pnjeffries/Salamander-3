using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Base;

namespace Salamander.BasicTools
{
    [Action("GetElementSelection", "Get the selection of elements in a saved set",
        IconBackground = Resources.URIs.ElementSet, IconForeground = Resources.URIs.GetIcon)]
    public class GetElementSelection : ModelDocumentActionBase
    {
        [ActionInput(1, "the name of the element set to retrieve",
            SuggestionsPath = "NameSuggestions")]
        public string Name { get; set; }

        public IList<string> NameSuggestions
        {
            get { return Model?.Sets?.ElementSets.GetNamesList(); }
        }

        [ActionOutput(1, "the elements in the set")]
        public ElementCollection Elements { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var set = Model.Sets.FindByName<ElementSet>(Name);
            if (set == null) PrintLine("No element set found named '" + Name + "'!");
            else Elements = set.Items;
            return true;
        }

        public override bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo == null && Elements != null) Core.Instance.Select(Elements);
            return true;
        }
    }
}
