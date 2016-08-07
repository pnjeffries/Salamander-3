﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;

namespace Newt.RhinoCommon
{
    /// <summary>
    /// The Rhino application host
    /// </summary>
    public class Host : IHost
    {
        #region Properties

        /// <summary>
        /// Backing member for the host Instance property
        /// </summary>
        private static Host _Instance;

        /// <summary>
        /// Get the current host singleton instance.
        /// Unlike the core itself, the host is auto-initialising.
        /// </summary>
        public static Host Instance
        {
            get
            {
                if (_Instance == null) _Instance = new Host();
                return _Instance;
            }
        }

        /// <summary>
        /// The Rhino GUI Controller
        /// </summary>
        public RhinoGUIController GUI { get; private set; }

        /// <summary>
        /// IHost GUI implementation
        /// </summary>
        GUIController IHost.GUI { get { return GUI; } }

        /// <summary>
        /// The Rhino Input Controller
        /// </summary>
        public InputController Input { get; private set; }

        /// <summary>
        /// IHost Input implementation
        /// </summary>
        InputController IHost.Input { get { return Input; } }


        #endregion

        #region Constructors

        /// <summary>
        /// Default private constructor
        /// </summary>
        private Host()
        {
            GUI = new RhinoGUIController();
            Input = new RhinoInputController();
        }

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
        public static bool EnsureInitialisation()
        {
            if (!Core.IsInitialised())
            {
                Core.Initialise(Instance);
            }
            return true;
        }

        public bool Print(string message)
        {
            RhinoApp.Write(message);
            return true;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}