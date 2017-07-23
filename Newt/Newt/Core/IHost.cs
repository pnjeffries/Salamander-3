using Nucleus.Base;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander
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
        /// Get the GUI controller that this host provides.
        /// </summary
        GUIController GUI { get; }

        /// <summary>
        /// Get the input controller class that this host provides.
        /// </summary>
        InputController Input { get; }

        /// <summary>
        /// Get the application avatar factory
        /// </summary>
        IAvatarFactory AvatarFactory { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 'Print' a message - displaying it in some form within the host
        /// application. 
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <returns>True if the message was printed successfully.</returns>
        bool Print(string message);

        /// <summary>
        /// Cause the host application to refresh any display elements which
        /// must be manually triggered.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Is the specified object currently not considered to be visible within the host environment?
        /// </summary>
        /// <param name="unique"></param>
        /// <returns></returns>
        bool IsHidden(Unique unique);

        #endregion
    }
}
