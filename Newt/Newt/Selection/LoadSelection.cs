using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// A selection of loads
    /// </summary>
    public class LoadSelection : SelectionViewModel<LoadCollection, Load>
    {
        /// <summary>
        /// Get or set the combined name value of the objects in this collection.
        /// If all the objects in this collection have the same name, that name will be returned.
        /// Otherwise the string "[Multi]" will be returned.
        /// Set this property to set the name property of all objects in this collection
        /// </summary>
        public virtual string Name
        {
            get { return CombinedValue(i => i.Name, "[Multi]"); }
            set { foreach (Load item in Selection) item.Name = value; }
        }

    }
}
