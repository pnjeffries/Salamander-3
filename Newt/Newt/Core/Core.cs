using Nucleus.Base;
using Nucleus.Model;
using Salamander.Actions;
using Salamander.Display;
using Salamander.Events;
using Salamander.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Salamander
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
                if (_ActiveDocument == null) NewDocument(true);
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

        /// <summary>
        /// The set of currently user-selected objects
        /// </summary>
        public SelectionSet Selected { get; } = new SelectionSet();

        /// <summary>
        /// The library of standard section profiles
        /// !!!TO BE REVIEWED!!!
        /// </summary>
        public SectionProfileLibrary SectionLibrary { get; } = new SectionProfileLibrary();

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
            _Instance.LoadAssets();
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

        private void LoadAssets()
        {
            FilePath directory = FilePath.DirectoryOf(Assembly.GetExecutingAssembly());

            // Load Section Library
            FilePath sectLibPath = directory + "\\Resources\\SectLib.csv";
            PrintLine("Loading Section library from " + sectLibPath);
            try
            {
                SectionLibrary.LoadFromCSV(sectLibPath);
            }
            catch (Exception ex)
            {
                PrintLine("Error during loading: " + ex.Message);
            }
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
                //PrintLine("Loading plugins from " + pluginFolder.FullName);
                FileInfo[] files = pluginFolder.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".s3a")
                    {
                        string filePath = file.FullName;
                        Assembly pluginAss = LoadAssemblyFromFile(filePath); //Assembly.LoadFrom(filePath);//
                        if (pluginAss != null)
                        {
                            PrintLine("Loading Salamander plugin '" + filePath + "'...");
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
                    else if (file.Extension == ".dll") //Load plugin libraries
                    {
                        string filePath = file.FullName;
                        Assembly pluginAss = LoadAssemblyFromFile(filePath);
                    }
                }
            }
        }

        /// <summary>
        /// Custom implementation to load assemblies from files.
        /// This can cause problems if used to load non-plugin assemblies,
        /// including, for some reason, with WPF binding.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static Assembly LoadAssemblyFromFile(string filePath)
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                if (!ReferenceEquals(stream, null))
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            return null;
        }

        /// <summary>
        /// Open a new, blank, design document and set it as the active design
        /// </summary>
        /// <returns></returns>
        public ModelDocument NewDocument(bool silent = false)
        {
            ModelDocument result = new ModelDocument();
            ActiveDocument = result;

            //TEMP:
            result.Model.Materials.Add(Material.Steel);
            result.Model.Materials.Add(Material.Concrete);
            result.Model.Materials.Add(Material.Aluminium);
            result.Model.Materials.Add(Material.Glass);
            result.Model.Materials.Add(Material.Wood);
            result.Model.LoadCases.Add(new LoadCase("Dead"));
            result.Model.LoadCases.Add(new LoadCase("Superimposed Dead"));
            result.Model.LoadCases.Add(new LoadCase("Live"));

            return result;
        }

        /// <summary>
        /// Import a document and merge it into the current active model.
        /// </summary>
        public void ImportDocument()
        {
            ModelDocument doc = OpenDocument(true);
            if (doc != null)
            {
                ActiveDocument.MergeIn(doc);
            }
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
                    Actions.ExecuteAction(importer, null, !background, false);
                    if (importer is IImportDocumentAction) result = ((IImportDocumentAction)importer).Document;
                }
            }
            return result;
        }

        /// <summary>
        /// Save the active model document to a file selected via a file dialog
        /// </summary>
        /// <returns></returns>
        public bool SaveDocument()
        {
            return SaveDocument(ActiveDocument);
        }

        /// <summary>
        /// Save the active model document to a file at the specified location
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SaveDocument(FilePath filePath)
        {
            return SaveDocument(filePath, ActiveDocument);
        }

        /// <summary>
        /// Save a model document to a file selected via a file dialog
        /// </summary>
        /// <param name="document">The document to be saved</param>
        /// <returns></returns>
        public bool SaveDocument(ModelDocument document)
        {
            string filters = Actions.GetExportFilters();
            string filePath = UI.ShowSaveFileDialog("Enter filepath to write to", filters, null, Actions.ExportFilterIndex(".s3b") + 1);
            if (!string.IsNullOrEmpty(filePath)) return SaveDocument(filePath, document);
            else return false;
        }

        /// <summary>
        /// Save a model document to a file
        /// </summary>
        /// <param name="filePath">The filepath to save to</param>
        /// <param name="document">The document to be saved</param>
        /// <returns>True if export command ran successfully</returns>
        public bool SaveDocument(FilePath filePath, ModelDocument document)
        {
            string extension = Path.GetExtension(filePath);
            IExportAction exporter = Actions.GetExporterFor(extension);
            if (exporter != null)
            {
                exporter.FilePath = filePath;
                exporter.Document = document;
                Actions.ExecuteAction(exporter, null, true, false);
                return true;
            }
            PrintLine("Error: No exporter loaded for extension '." + extension + "'.  File could not be written.");
            return false;
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

        /// <summary>
        /// Add the specified list of objects to the current selection
        /// </summary>
        /// <param name="items"></param>
        /// <param name="clear"></param>
        /// <param name="inHost"></param>
        /// <returns></returns>
        public bool Select(IList items, bool clear = false, bool inHost = true)
        {
            bool result = false;
            if (clear) Selected.Clear();
            foreach (object item in items)
            {
                if (item is ModelObject)
                {
                    if (Selected.Select((ModelObject)item)) result = true;
                }
            }
            if (inHost)
            {
                if (Host.Select(items, clear)) result = true;
            }
            return result;
        }

        #endregion
    }
}
