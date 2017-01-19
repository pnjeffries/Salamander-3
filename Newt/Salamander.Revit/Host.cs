using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salamander.Display;

namespace Salamander.Revit
{
    /// <summary>
    /// The Revit application host
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

        IAvatarFactory IHost.AvatarFactory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        GUIController IHost.GUI
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        InputController IHost.Input
        {
            get
            {
                throw new NotImplementedException();
            }
        }



        #endregion

        #region Methods

        public bool Print(string message)
        {
            //throw new NotImplementedException();
            return false;
        }

        public void Refresh()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
