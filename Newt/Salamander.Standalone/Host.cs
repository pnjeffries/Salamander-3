using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Base;
using Salamander.Display;
using Salamander.UI;
using System.Collections.ObjectModel;

namespace Salamander.Standalone
{
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
        /// Private flag set to true once the splash screen has been displayed
        /// </summary>
        private bool _SplashShown = false;

        public GUIController GUI { get; private set; }

        public InputController Input { get; private set; }

        public IAvatarFactory AvatarFactory { get; private set; }

        /// <summary>
        /// The message log used to display messages
        /// </summary>
        public ObservableCollection<string> Log { get; private set; }

        #endregion

        #region Constructors

        private Host()
        {
            Log = new ObservableCollection<string>();
            GUI = new WPFGUIController();
            AvatarFactory = new WPFAvatarFactory();
        }

        #endregion

        #region Methods

        public bool IsHidden(Unique unique)
        {
            return false;
        }

        public bool Print(string message)
        {
            App.Current.Dispatcher.Invoke(new Action(() => Log.Add(message)));
            return true;
        }

        public void PrintOverLast(string message)
        {
            App.Current.Dispatcher.Invoke(new Action(() => Log[Log.Count - 1] = message));
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public bool Select(IList items, bool clear = false)
        {
            throw new NotImplementedException();
        }

        private void PreInitialise(bool quiet)
        {
            if (!quiet)
            {
                GUI.ShowSplashScreen();
                _SplashShown = true;
            }
        }

        private void Initialise(bool quiet)
        {

            /*Input = new RhinoInput();
            AvatarFactory = new RhinoAvatarFactory();
            Handles = new HandlesLayer();
            Core.Instance.Layers.Layers.Add(Handles); //TEST
            DisplayConduit = new SalamanderDisplayConduit();
            DisplayConduit.Enabled = true;

            Core.Instance.ActiveModelChanged += Core_ChangeDocument;

            RhinoDoc.CloseDocument += RhinoDoc_CloseDocument;

            if (!quiet) GUI.CreateHostDockPanel(); //TEMP?
            */
        }

        #endregion

        #region Static Methods

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
        public static bool EnsureInitialisation(bool quiet = false)
        {
            if (!Core.IsInitialised())
            {
                //Instance.PreInitialise(quiet);
                Core.Initialise(Instance);
                //Instance.Initialise(quiet);
            }
            else if (!Instance._SplashShown && !quiet)
            {
                Instance._SplashShown = true;
                Instance.GUI.ShowSplashScreen();
                //Instance.GUI.CreateHostDockPanel();
            }
            return true;
        }

        #endregion
    }
}
