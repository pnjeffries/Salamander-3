using Nucleus.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Salamander.Rhino
{
    /// <summary>
    /// A mapping table for Salamander object IDs to Rhino 'handle' object IDs
    /// </summary>
    [Serializable]
    public class RhinoIDMappingTable : IDMappingTable<Guid, Guid>
    {
        /// <summary>
        /// The name of the category under which handle links are to be stored
        /// </summary>
        public string HandlesCategory { get { return "Handles"; } }

        public RhinoIDMappingTable() : base("Salamander", "Rhino")
        {
        }

        protected RhinoIDMappingTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
