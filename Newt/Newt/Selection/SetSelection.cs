using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    public class SetSelection : SelectionViewModel<ModelObjectSetCollection, ModelObjectSetBase>
    {
        /// <summary>
        /// The primary selected set for individual property display - will be the last selected set
        /// </summary>
        public ModelObjectSetBase SelectedSet
        {
            get
            {
                if (Selection.Count > 0) return Selection.Last();
                else return null;
            }
        }
    }
}
