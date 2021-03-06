﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Rhino;
using Salamander.Rhino;
using Salamander.Events;
using Salamander.Display;
using Nucleus.Base;
using System.Collections;
using Nucleus.Model;
using Nucleus.Rhino;

namespace Salamander.RhinoCommon
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
        /// Private flag set to true once the splash screen has been displayed
        /// </summary>
        private bool _SplashShown = false;

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

        /// <summary>
        /// The manager which maintains links between Rhino object 'handles'
        /// and model objects
        /// </summary>
        public HandlesLayer Handles { get; private set; }

        /// <summary>
        /// The factory class which generates display avatars
        /// </summary>
        public RhinoAvatarFactory AvatarFactory { get; private set; }

        /// <summary>
        /// IHost AvatarFactory implementation
        /// </summary>
        IAvatarFactory IHost.AvatarFactory { get { return AvatarFactory; } }

        /// <summary>
        /// The display conduit used to draw in the Rhino viewport
        /// </summary>
        public SalamanderDisplayConduit DisplayConduit { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default private constructor
        /// </summary>
        private Host()
        {
            
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
        public static bool EnsureInitialisation(bool quiet = false)
        {
            if (!Core.IsInitialised())
            {
                Instance.PreInitialise(quiet);
                Core.Initialise(Instance);
                Instance.Initialise(quiet);
            }
            else if (!Instance._SplashShown && !quiet)
            {
                Instance._SplashShown = true;
                Instance.GUI.ShowSplashScreen();
                Instance.GUI.CreateHostDockPanel();
            }
            return true;
        }

        private void PreInitialise(bool quiet)
        {
            GUI = new RhinoGUIController();
            GUI.HostWindowHandle = RhinoApp.MainWindowHandle();
            if (!quiet)
            {
                GUI.ShowSplashScreen();
                _SplashShown = true;
            }
        }

        private void Initialise(bool quiet)
        {
            
            Input = new RhinoInput();
            AvatarFactory = new RhinoAvatarFactory();
            Handles = new HandlesLayer();
            Core.Instance.Layers.Layers.Add(Handles); //TEST
            DisplayConduit = new SalamanderDisplayConduit();
            DisplayConduit.Enabled = true;

            Core.Instance.ActiveModelChanged += Core_ChangeDocument;

            RhinoDoc.CloseDocument += RhinoDoc_CloseDocument;

            if (!quiet) GUI.CreateHostDockPanel(); //TEMP?
        }

        public bool Print(string message)
        {
            RhinoApp.Write(message);
            return true;
        }

        /// <summary>
        /// The timer that is used to defer refreshes
        /// </summary>
        private DispatcherTimer _RefreshTimer = null;

        /// <summary>
        /// Refresh the Rhino viewports.
        /// The actual refresh is delayed by a fraction of a second to
        /// prevent multiple refreshes in quick succession.
        /// </summary>
        public void Refresh()
        {
            if (!_Redrawing)
            {  
                if (_RefreshTimer == null)
                {
                    _RefreshTimer = new DispatcherTimer();
                    _RefreshTimer.Interval = new TimeSpan(10000);
                    _RefreshTimer.Tick += RefreshTimer_Tick;
                }
                _RefreshTimer.Stop();
                _RedrawCount = 0;
                _RefreshTimer.Start();
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            _RedrawCount++;
            if (_RedrawCount > 10)
            {
                _RedrawCount = 0;
                _RefreshTimer.Stop();
                if (_Redrawing == false)
                {
                    _Redrawing = true;
                    RhinoDoc.ActiveDoc.Views.Redraw();
                    _Redrawing = false;
                }
            }
        }

        private bool _Redrawing = false;
        private int _RedrawCount = 0;

        public bool IsHidden(Unique unique)
        {
            return Handles.IsHandleHidden(unique);
        }

        public bool Select(IList items, bool clear = false)
        {
            if (clear) RhinoOutput.UnselectAllObjects();
            foreach (object item in items)
            {
                if (item is ModelObject)
                {
                    Handles.SelectHandle((ModelObject)item);
                }
            }
            return true;
        }

        #endregion

        #region Event Handlers

        private void RhinoDoc_CloseDocument(object sender, DocumentEventArgs e)
        {
            Core.Instance.NewDocument();
        }

        private void Core_ChangeDocument(object sender, DocumentOpenedEventArgs e)
        {

        }

        #endregion
    }
}
