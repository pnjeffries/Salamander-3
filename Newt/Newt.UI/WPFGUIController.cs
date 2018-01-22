using Microsoft.Win32;
using Salamander.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nucleus.UI;
using Nucleus.WPF;
using System.Windows.Interop;

namespace Salamander.UI
{
    /// <summary>
    /// GUI controller for WPF UI Elements
    /// </summary>
    public class WPFGUIController : GUIController
    {
        /// <summary>
        /// The HWND handle of the parent host window
        /// </summary>
        public IntPtr HostWindowHandle { get; set; } = IntPtr.Zero;

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
            CreateContainerWindow(name, new DataTable(data, selection), 640, 480);
        }

        /// <summary>
        /// Create a window as a container for another FrameworkElement
        /// </summary>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        protected Window CreateContainerWindow(string title, FrameworkElement contents)
        {
            Window window = new Window();
            window.Content = contents;
            window.Title = title;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            if (HostWindowHandle != IntPtr.Zero)
            {
                WindowInteropHelper wIH = new WindowInteropHelper(window);
                wIH.Owner = HostWindowHandle;
            }
            else
            {
                window.Topmost = true;
            }
            window.Show();
            return window;
        }

        /// <summary>
        /// Create a window as a container for another FrameworkElement
        /// </summary>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        protected Window CreateContainerWindow(string title, FrameworkElement contents, double width, double height)
        {
            Window window = new Window();
            window.Content = contents;
            window.Title = title;
            if (HostWindowHandle != IntPtr.Zero)
            {
                WindowInteropHelper wIH = new WindowInteropHelper(window);
                wIH.Owner = HostWindowHandle;
            }
            else
            {
                window.Topmost = true;
            }
            window.SizeToContent = SizeToContent.Manual;
            window.Width = width;
            window.Height = height;
            window.Show();
            return window;
        }

        /// <summary>
        /// Show a message dialog with optional options
        /// </summary>
        /// <param name="title">Text to be shown in the dialog title bar</param>
        /// <param name="message">Message to be shown in the dialog</param>
        /// <param name="options">Options to be displayed in the dialog</param>
        /// <returns>The returnvalue of the selected option, or null if no options are provided</returns>
        public override object ShowDialog(string title, string message, params UIOption[] options)
        {
            if (options.Length == 0)
            {
                MessageDialog.Show(title, message);
                return null;
            }
            else
                return MessageDialog.ShowOptions(title, message, options);
        }

        /// <summary>
        /// Show a dialog hosting auto-generated UI Elements allowing editing of the fields
        /// on the specified object marked with the AutoUI attribute.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool? ShowFieldsDialog(string title, object options)
        {
            var window = new AutoUIDialog(title, options);
            return window.ShowDialog();
        }

        /// <summary>
        /// Show a dialog that allows the user to enter text, optionally selecting a string
        /// from a list of suggestions.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="suggestions"></param>
        /// <returns></returns>
        public override bool? ShowTextDialog(string title, ref string text, IList<string> suggestions = null)
        {
            return TextComboDialog.Show(title, ref text, suggestions);
        }
    }
}
