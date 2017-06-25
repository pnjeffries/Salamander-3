using Nucleus.Base;
using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// A selection of Material objects
    /// </summary>
    public class MaterialSelection : SelectionViewModel<MaterialCollection, Material>
    {
        /// <summary>
        /// Get or set the combined name value of the objects in this collection.
        /// If all the objects in this collection have the same name, that name will be returned.
        /// Otherwise the string "[Multi]" will be returned.
        /// Set this property to set the name property of all objects in this collection
        /// </summary>
        public virtual string Name
        {
            get { return (string)CombinedValue(i => i.Name, "[Multi]"); }
            set { foreach (Named item in Selection) item.Name = value; }
        }
    }
}
