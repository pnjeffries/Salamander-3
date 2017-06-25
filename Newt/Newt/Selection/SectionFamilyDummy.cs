using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// Class for dummy section properties.
    /// </summary>
    public class SectionFamilyDummy : SectionFamily
    {
        #region Constructors

        /// <summary>
        /// Initialise a new dummy section property with the specified name
        /// </summary>
        /// <param name="name"></param>
        public SectionFamilyDummy(string name) { Name = name; }

        #endregion
    }
}
