﻿using Salamander.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander
{
    /// <summary>
    /// Abstract base class for GUI Controller classes.
    /// GUIController implementations allow graphical user interface elements
    /// to be created, displayed, accessed and so on.
    /// </summary>
    public abstract class GUIController
    {
        /// <summary>
        /// An event raised when UI initialisation has been completed
        /// </summary>
        public event EventHandler UIInitialisationCompleted;

        /// <summary>
        /// Notify the controller that application initialisation has completed
        /// </summary>
        public void NotifyInitialisationCompleted()
        {
            UIInitialisationCompleted(this, null);
        }

        /// <summary>
        /// Show an OpenFileDialog and use it to obtain a filepath
        /// </summary>
        /// <param name="prompt">The save file prompt message</param>
        /// <param name="filter">The filetype filter string</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public abstract string ShowOpenFileDialog(string prompt, string filter, string defaultPath = null, int filterIndex = -1);

        /// <summary>
        /// Show a SaveFileDialog and use it to prompt the user to enter a filepath
        /// </summary>
        /// <param name="prompt">Text to be shown in the dialog title bar</param>
        /// <param name="filter">Filetype filter text</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public abstract string ShowSaveFileDialog(string prompt, string filter, string defaultPath = null, int filterIndex = -1);

        /// <summary>
        /// Show the application splash scren
        /// </summary>
        public abstract void ShowSplashScreen();

        /// <summary>
        /// Show a data table for the specified data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public abstract void ShowDataTable(string name, IEnumerable data, SelectionViewModel selection = null);

    }
}
