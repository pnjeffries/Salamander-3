using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;

namespace Salamander.BasicTools
{
    [Action("SaveElementSelection", "Save a selection of elements as a named element set",
        IconBackground = Resources.URIs.ElementSet, IconForeground = Resources.URIs.AddIcon)]
    public class SaveElementSelection : ModelDocumentActionBase
    {
        [ActionInput(1, "the element selection to be saved as a Set", OneByOne = false)]
        public ElementCollection Elements { get; set; }

        [ActionInput(2, "the name of the set to be saved under.  This will replace any existing element set with the same name.")]
        public string Name { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            ElementSet set = Model.Sets.FindOrCreate<ElementSet>(Name);
            set.Set(Elements);
            return true;
        }
    }
}
