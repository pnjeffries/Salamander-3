using FreeBuild.Base;
using FreeBuild.Model;
using Newt.Actions;
using Newt.Display;
using Newt.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newt
{
    /// <summary>
    /// The core manager class.
    /// Deals with general file and data handline and overall top-level
    /// applicaton management.
    /// Implemented as a singleton - call Core.Instance to access the active
    /// core instance
    /// </summary>
    public class Core : NotifyPropertyChangedBase
    {
        #region Events

        /// <summary>
        /// The currently active model document has changed
        /// </summary>
        public EventHandler<DocumentOpenedEventArgs> ActiveModelChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Private internal singleton instance
        /// </summary>
        private static Core _Instance = null;

        /// <summary>
        /// Get the singleton instance of the core object.
        /// If the core has not yet been initialised this will return null.
        /// Call EnsureInitialisation() on the application host object before
        /// accessing this if you are unsure whether initialisation has already
        /// taken place.
        /// </summary>
        public static Core Instance { get { return _Instance; } }

        /// <summary>
        /// Get the host application interface
        /// </summary>
        public IHost Host { get; }

        /// <summary>
        /// The action manager class.
        /// Use this to load and call plug-in actions.
        /// </summary>
        public ActionManager Actions { get; }

        /// <summary>
        /// The display layer manager class.
        /// </summary>
        public DisplayLayerManager Layers { get; }

        /// <summary>
        /// The controller for UI operations
        /// </summary>
        public GUIController UI { get { return Host.GUI; } }

        /// <summary>
        /// Private backing field for the ActiveDocument property
        /// </summary>
        private ModelDocument _ActiveDocument;

        /// <summary>
        /// The currently active open document
        /// </summary>
        public ModelDocument ActiveDocument
        {
            get
            {
                if (_ActiveDocument == null) ActiveDocument = new ModelDocument();
                return _ActiveDocument;
            }
            set
            {
                _ActiveDocument = value;
                NotifyPropertyChanged("ActiveDocument");
                Layers.RegisterModel(_ActiveDocument.Model);
                RaiseEvent(ActiveModelChanged, new DocumentOpenedEventArgs(_ActiveDocument));
            }
        }

        /// <summary>
        /// Private backing field for the OpenDocuments property
        /// </summary>
        private ModelDocumentCollection _OpenDocuments = null;

        /// <summary>
        /// The full collection of all currently loaded model documents
        /// </summary>
        public ModelDocumentCollection OpenDocuments
        {
            get
            {
                if (_OpenDocuments == null)
                {
                    //Lazy initialisation:
                    _OpenDocuments = new ModelDocumentCollection();
                }
                return _OpenDocuments;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="host"></param>
        private Core(IHost host)
        {
            Host = host;
            Actions = new ActionManager();
            Layers = new DisplayLayerManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialise the core object to the specified host.
        /// </summary>
        /// <param name="host">An object which implements the IHost interface
        /// to allow the Core to interact with the hosting application.</param>
        /// <returns></returns>
        public static void Initialise(IHost host)
        {
            if (_Instance == null) _Instance = new Core(host);
            _Instance.LoadPlugins();
        }

        /// <summary>
        /// Is the core initialised?
        /// </summary>
        /// <returns>True if the core instance is initialised and available to access.
        /// Else, false.</returns>
        public static bool IsInitialised()
        {
            return Instance != null;
        }

        /// <summary>
        /// Open a new, blank, design document and set it as the active design
        /// </summary>
        /// <returns></returns>
        public ModelDocument NewDocument(bool silent = false)
        {
            ModelDocument result = new ModelDocument();
            ActiveDocument = result;
            return result;
        }

        /// <summary>
        /// Open a design document from a file selected via a file dialog
        /// </summary>
        /// <param name="background">If true, the opened design will not be added to the set of currently open designs or made active</param>
        /// <returns>The opened design document, or null if one was not opened.</returns>
        public ModelDocument OpenDocument(bool background = false)
        {
            string filters = Actions.GetImportFilters();
            string filePath = UI.ShowOpenFileDialog("Select File To Open", filters);
            if (!string.IsNullOrEmpty(filePath)) return OpenDocument(filePath, background);
            else return null;
        }

        /// <summary>
        /// Open a design document from a file
        /// </summary>
        /// <param name="filePath">The filepath to open</param>
        /// <param name="background">If true, the opened design will not be added to the set of currently open designs or made active</param>
        /// <returns></returns>
        public ModelDocument OpenDocument(string filePath, bool background = false)
        {
            ModelDocument result = null;
            if (File.Exists(filePath))
            {
                string extension = Path.GetExtension(filePath);
                IImportAction importer = Actions.GetImporterFor(extension);
                if (importer != null)
                {
                    importer.FilePath = filePath;
                    Actions.ExecuteAction(importer, null, !background);
                    if (importer is IImportDocumentAction) result = ((IImportDocumentAction)importer).Document;
                }
            }
            return result;
        }

        /// <summary>
        /// Load all plugin assemblies
        /// </summary>
        private void LoadPlugins()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo executing = new FileInfo(location);
            DirectoryInfo directory = executing.Directory;
            DirectoryInfo[] subDirectories = directory.GetDirectories("Plugins");
            foreach (DirectoryInfo pluginFolder in subDirectories)
            {
                FileInfo[] files = pluginFolder.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".dll")
                    {
                        string filePath = file.FullName;
                        Assembly pluginAss = Assembly.LoadFrom(filePath);
                        if (pluginAss != null)
                        {
                            PrintLine("Loading Newt plugin '" + filePath + "'...");
                            //Load Actions:
                            Actions.LoadPlugin(pluginAss);
                            //Load Layers:
                            Layers.LoadPlugin(pluginAss);
                        }
                        else
                        {
                            PrintLine("Warning: Could not load assembly '" + filePath + "'!");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Execute the action with the given command name
        /// </summary>
        /// <param name="commandName">The command name to execute</param>
        /// <returns>The executed action if one with that command name is loaded and could be run
        /// successfully, else null.</returns>
        public IAction Execute(string commandName)
        {
            return Actions.ExecuteAction(commandName);
        }

        /// <summary>
        /// Print a message output.
        /// This is a shortcut method that reaches through to the Instance.Host.Print method.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        public static bool Print(string message)
        {
            return Instance.Host.Print(message);
        }

        /// <summary>
        /// Print a message output followed by a newline character.
        /// This is a shortcut method that reaches through to the Instance.Host.Print method.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        public static bool PrintLine(string message)
        {
            return Instance.Host.Print(message + Environment.NewLine);
        }

        #endregion
    }
}
