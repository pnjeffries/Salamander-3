using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt
{
    /// <summary>
    /// Abstract base class for GUI Controller classes.
    /// GUIController implementations allow graphical user interface elements
    /// to be created, displayed, accessed and so on.
    /// </summary>
    public abstract class GUIController
    {
        /// <summary>
        /// Show an OpenFileDialog and use it to obtain a filepath
        /// </summary>
        /// <param name="prompt">The save file prompt message</param>
        /// <param name="filter">The filetype filter string</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public abstract string ShowOpenFileDialog(string prompt, string filter, string defaultPath = null);

        /// <summary>
        /// Show a SaveFileDialog and use it to prompt the user to enter a filepath
        /// </summary>
        /// <param name="prompt">Text to be shown in the dialog title bar</param>
        /// <param name="filter">Filetype filter text</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public abstract string ShowSaveFileDialog(string prompt, string filter, string defaultPath = null);

    }
}
