using FreeBuild.Base;
using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt
{
    /// <summary>
    /// The core manager class.
    /// Deals with general file and data handline and overall top-level
    /// applicaton management.
    /// Implemented as a singleton - call Core.Instance to access the current
    /// </summary>
    public class Core : NotifyPropertyChangedBase
    {
        #region Properties

        /// <summary>
        /// Private internal singleton instance
        /// </summary>
        private static Core _Instance = null;

        /// <summary>
        /// Get the singleton instance of the core object.
        /// If the core has not yet been initialised this will return null.
        /// Call EnsureInitialisation() on the application host object before
        /// accessing this if you are unsure whether initialisation has already
        /// taken place.
        /// </summary>
        public static Core Instance { get { return _Instance; } }

        /// <summary>
        /// Get the host application interface
        /// </summary>
        public IHost Host { get; }

        /// <summary>
        /// The currently active open document
        /// </summary>
        public ModelDocument ActiveDocument { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="host"></param>
        private Core(IHost host)
        {
            Host = host;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialise the core object to the specified host.
        /// </summary>
        /// <param name="host">An object which implements the IHost interface
        /// to allow the Core to interact with the hosting application.</param>
        /// <returns></returns>
        public static void Initialise(IHost host)
        {
            _Instance = new Core(host);
        }

        /// <summary>
        /// Is the core initialised?
        /// </summary>
        /// <returns>True if the core instance is initialised and available to access.
        /// Else, false.</returns>
        public static bool IsInitialised()
        {
            return Instance != null;
        }

        #endregion
    }
}
