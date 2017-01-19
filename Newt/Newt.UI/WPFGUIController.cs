using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.UI
{
    /// <summary>
    /// GUI controller for WPF UI Elements
    /// </summary>
    public class WPFGUIController : GUIController
    {
        /// <summary>
        /// Show an OpenFileDialog and use it to obtain a filepath
        /// </summary>
        /// <param name="prompt">The save file prompt message</param>
        /// <param name="filter">The filetype filter string</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public override string ShowOpenFileDialog(string prompt, string filter, string defaultPath = null, int filterIndex = 0)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = prompt;
            dialog.Filter = filter;
            if (filterIndex > 0) dialog.FilterIndex = filterIndex;
            if (defaultPath != null) dialog.FileName = defaultPath;
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else return null;
        }

        /// <summary>
        /// Show a SaveFileDialog and use it to prompt the user to enter a filepath
        /// </summary>
        /// <param name="prompt">Text to be shown in the dialog title bar</param>
        /// <param name="filter">Filetype filter text</param>
        /// <returns>The selected filepath.  Or, null if the dialog is closed
        /// without a filepath being selected.</returns>
        public override string ShowSaveFileDialog(string prompt, string filter, string defaultPath = null, int filterIndex = 0)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = prompt;
            dialog.Filter = filter;
            if (filterIndex > 0) dialog.FilterIndex = filterIndex;
            if (defaultPath != null) dialog.FileName = defaultPath;
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else return null;
        }
    }
}
