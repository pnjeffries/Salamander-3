using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Rhino
{
    /// <summary>
    /// The Rhino application host
    /// </summary>
    public class Host : IHost
    {
        /// <summary>
        /// Backing member for the host Instance property
        /// </summary>
        private static Host _Instance;

        /// <summary>
        /// Get the current host instance
        /// </summary>
        public static Host Instance
        {
            get
            {
                if (_Instance == null) _Instance = new Host();
                return _Instance;
            }
        }

        public UIManager UI
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool EnsureInitialisation()
        {
            if (!Core.IsInitialised())
            {
                Core.Initialise(this);
            }
            return true;
        }

        public bool Print(string message)
        {
            throw new NotImplementedException();
        }
    }
}
