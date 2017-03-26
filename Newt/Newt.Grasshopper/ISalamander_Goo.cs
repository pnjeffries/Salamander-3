using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Interface for GH_Goo implementations for Salamander object types
    /// </summary>
    public interface ISalamander_Goo : IGH_Goo
    {
        /// <summary>
        /// The wrapped goo value
        /// </summary>
        object GetValue(Type type);
    }
}
