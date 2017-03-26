using Microsoft.Win32;
using Salamander.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        /// <summary>
        /// Show the application splash screen
        /// </summary>
        public override void ShowSplashScreen()
        {
            SplashScreen ss = new SplashScreen(this);
            ss.Show();
        }

        /// <summary>
        /// Show a data table for the specified data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public override void ShowDataTable(string name, IEnumerable data, SelectionViewModel selection = null)
        {
            CreateContainerWindow(name, new DataTable(data, selection));
        }

        /// <summary>
        /// Create a window as a container for another FrameworkElement
        /// </summary>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        private static Window CreateContainerWindow(string title, FrameworkElement contents)
        {
            Window window = new Window();
            window.Content = contents;
            window.Title = title;
            window.Topmost = true;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Show();
            return window;
        }
    }
}
