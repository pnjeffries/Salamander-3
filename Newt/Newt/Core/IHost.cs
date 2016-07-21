using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt
{
    /// <summary>
    /// Interface for application hosts.
    /// Application hosts are responsible for initialising the core
    /// instance and for linking to key interaction logic within
    /// the target application - for example selection of objects,
    /// 3D rendering, user interface creation and so on.
    /// </summary>
    public interface IHost
    {
        #region Properties

        /// <summary>
        /// Get the UI manager that this host provides
        /// </summary>
        UIManager UI { get; }

        #endregion

        #region Methods

        /// <summary>
        /// This function ensures that the application core is initialised.
        /// It should be called in any circumstance where core functionality
        /// is necessary, but where it cannot be presumed that this 
        /// initialisation has already been performed, before the core itself
        /// is accessed.
        /// </summary>
        /// <returns>True if the application has been successfully initialised
        /// (either this time or previously) and it is OK to proceed.  False
        /// if something went wrong during the initialisation process and whatever
        /// you were hoping to do should be aborted.</returns>
        bool EnsureInitialisation();

        /// <summary>
        /// 'Print' a message - displaying it in some form within the host
        /// application. 
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <returns>True if the message was printed successfully.</returns>
        bool Print(string message);

        #endregion
    }
}
