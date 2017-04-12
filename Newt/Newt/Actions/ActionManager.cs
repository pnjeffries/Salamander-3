using FreeBuild.Base;
using FreeBuild.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Manager class for Salamander actions
    /// </summary>
    public class ActionManager : NotifyPropertyChangedBase
    {
        /// <summary>
        /// A registry of all loaded actions
        /// </summary>
        private Dictionary<string, Type> LoadedActions { get; set; } = new Dictionary<string, Type>();

        /// <summary>
        /// A registry of export actions keyed by the extension string (in lower case, including the '.')
        /// </summary>
        public Dictionary<string, Type> ExtensionExporters { get; } = new Dictionary<string, Type>();

        /// <summary>
        /// A registry of import actions keyed by the extension string (in lower case, including the '.')
        /// </summary>
        public Dictionary<string, Type> ExtensionImporters { get; } = new Dictionary<string, Type>();

        /// <summary>
        /// The last command manually activated
        /// </summary>
        public IAction LastCommand { get; set; } = null;

        /// <summary>
        /// Stores the last action performed for each command name, so that settings on persistant inputs can be restored
        /// </summary>
        public Dictionary<string, IAction> PreviousActions { get; } = new Dictionary<string, IAction>();

        /// <summary>
        /// Private backing field for CurrentAction 
        /// </summary>
        private IAction _CurrentAction = null;

        /// <summary>
        /// The currently active action
        /// </summary>
        public IAction CurrentAction
        {
            get { return _CurrentAction; }
            set
            {
                _CurrentAction = value;
                NotifyPropertyChanged("CurrentAction");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionManager()
        {

        }

        /// <summary>
        /// Load a plugin action set from an assembly
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadPlugin(Assembly pluginAss)
        {
            if (pluginAss != null)
            {
                Type[] types = pluginAss.GetTypes();
                foreach (Type type in types)
                {
                    if (typeof(IAction).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        string commandName;
                        object[] typeAtts = type.GetCustomAttributes(typeof(ActionAttribute), false);
                        if (typeAtts.Count() > 0)
                        {
                            ActionAttribute aAtt = (ActionAttribute)typeAtts[0];
                            commandName = aAtt.CommandName;
                        }
                        else commandName = type.Name;

                        if (LoadedActions.ContainsKey(commandName))
                        {
                            Core.PrintLine("Could not load action with command name '" + commandName +
                                "' from assembly '" + pluginAss.Location + "' - an action with the same name is already loaded");
                        }
                        else
                        {
                            LoadedActions.Add(commandName, type);

                            //Importer actions
                            if (typeof(IImportAction).IsAssignableFrom(type))
                            {

                                ImportActionAttribute importAtt = ImportActionAttribute.ExtractFrom(type);
                                if (importAtt != null)
                                {
                                    foreach (string extension in importAtt.Extensions)
                                    {
                                        string ext = extension.ToLower();
                                        if (ExtensionImporters.ContainsKey(ext))
                                            Core.PrintLine("Warning: Importer for file extension '" + extension + "' is already loaded.  Extension is ambiguous.");
                                        ExtensionImporters[ext] = type;
                                    }
                                }
                            }

                            //Exporter actions:
                            if (typeof(IExportAction).IsAssignableFrom(type))
                            {

                                ExportActionAttribute exportAtt = ExportActionAttribute.ExtractFrom(type);
                                if (exportAtt != null)
                                {
                                    foreach (string extension in exportAtt.Extensions)
                                    {
                                        string ext = extension.ToLower();
                                        if (ExtensionExporters.ContainsKey(ext))
                                            Core.PrintLine("Warning: Exporter for file extension '" + extension + "' is already loaded.  Extension is ambiguous.");
                                        ExtensionExporters[ext] = type;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Get the type of the action with the specified command name
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public Type GetActionDefinition(string commandName)
        {
            if (LoadedActions.ContainsKey(commandName))
            {
                return LoadedActions[commandName];
            }
            return null;
        }

        /// <summary>
        /// Execute a loaded action with the given command name
        /// </summary>
        /// <param name="commandName">The name of the command to be executed.
        /// If left blank, the last executed command will be run again.</param>
        /// <returns></returns>
        public IAction ExecuteAction(string commandName, object context = null)
        {
            IAction action = null;
            if (LoadedActions.ContainsKey(commandName))
            {
                Type type = LoadedActions[commandName];
                action = (IAction)Activator.CreateInstance(type, true);
            }
            else if (string.IsNullOrWhiteSpace(commandName)) action = LastCommand;
            else
            {
                Core.PrintLine("Newt command '" + commandName + "' is not loaded or is not available within this host environment.");
            }
            return ExecuteAction(action, context);
        }

        /// <summary>
        /// Execute the given action
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IAction ExecuteAction(IAction action, object context = null, bool performPostExecutionOperations = true)
        {
            if (action != null)
            {
                CurrentAction = action;
                string commandName = action.GetCommandName();
                try
                {
                    if (action is IContextualAction)
                    {
                        ((IContextualAction)action).PopulateInputsFromContext(context);
                    }
                    if (PreviousActions.ContainsKey(commandName))
                    {
                        action.CopyPersistentValuesFrom(PreviousActions[commandName]);
                    }
                    if (action.PromptUserForInputs())
                    {
                        if (action.PreExecutionOperations())
                        {
                            //Core.Instance.Undo.BeginStage();
                            action.Execute();
                            if (performPostExecutionOperations) action.PostExecutionOperations();
                            //Core.Instance.Undo.EndStage();
                            Core.Instance.Host.Refresh();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Core.PrintLine("Error: Newt action '" + commandName + "' failed to complete.");
                    Core.PrintLine(ex.Message);
                    Core.PrintLine(ex.StackTrace);
                }
                PreviousActions[commandName] = action;
                LastCommand = action;
                CurrentAction = null;
            }
            return action;
        }

        /// <summary>
        /// Retrieve the default exporter for the given file type
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public IExportAction GetExporterFor(string extension)
        {
            extension = extension.ToLower();
            if (ExtensionExporters.ContainsKey(extension))
            {
                Type type = ExtensionExporters[extension];
                if (type != null) return (IExportAction)Activator.CreateInstance(type, true);
            }
            return null;
        }

        /// <summary>
        /// Retrieve the default importer for the given file type
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public IImportAction GetImporterFor(string extension)
        {
            extension = extension.ToLower();
            if (ExtensionImporters.ContainsKey(extension))
            {
                Type type = ExtensionImporters[extension];
                if (type != null) return (IImportAction)Activator.CreateInstance(type, true);
            }
            return null;
        }

        /// <summary>
        /// Returns a list of all exporter filters combined into one string
        /// </summary>
        /// <returns></returns>
        public string GetExportFilters()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbX = new StringBuilder(); //All extensions set

            string separator = "";
            string separatorX = "";
            foreach (KeyValuePair<string, Type> kvp in LoadedActions)
            {
                Type t = kvp.Value;
                if (typeof(IExportAction).IsAssignableFrom(t))
                {
                    ExportActionAttribute exportAtt = ExportActionAttribute.ExtractFrom(t);
                    if (exportAtt != null)
                    {
                        sb.Append(separator);
                        sb.Append(exportAtt.Filter);
                        separator = "|";
                        foreach (string ext in exportAtt.Extensions)
                        {
                            sbX.Append(separatorX);
                            sbX.Append("*");
                            sbX.Append(ext);
                            separatorX = "; ";
                        }
                    }
                }
            }
            return sb.ToString(); //"All compatible file types (" + sbX.ToString() + ")|" + sbX.ToString() + "|" + sb.ToString();
        }

        /// <summary>
        /// Find the index of the specified import filter from loaded importers
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public int ExportFilterIndex(string extension)
        {
            int count = -1;
            foreach (KeyValuePair<string, Type> kvp in LoadedActions)
            {
                Type t = kvp.Value;
                if (typeof(IExportAction).IsAssignableFrom(t))
                {
                    ExportActionAttribute exportAtt = ExportActionAttribute.ExtractFrom(t);
                    if (exportAtt != null)
                    {
                        count++;
                        foreach (string ext in exportAtt.Extensions)
                        {
                            if(extension.EqualsIgnoreCase(ext)) return count;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns a list of all importer filters combined into one string
        /// </summary>
        /// <returns></returns>
        public string GetImportFilters()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbX = new StringBuilder(); //All extensions set

            string separator = "";
            string separatorX = "";
            foreach (KeyValuePair<string, Type> kvp in LoadedActions)
            {
                Type t = kvp.Value;
                if (typeof(IImportAction).IsAssignableFrom(t))
                {
                    ImportActionAttribute importAtt = ImportActionAttribute.ExtractFrom(t);
                    if (importAtt != null)
                    {
                        sb.Append(separator);
                        sb.Append(importAtt.Filter);
                        separator = "|";
                        foreach (string ext in importAtt.Extensions)
                        {
                            sbX.Append(separatorX);
                            sbX.Append("*");
                            sbX.Append(ext);
                            separatorX = "; ";
                        }
                    }
                }
            }
            return "All compatible file types (" + sbX.ToString() + ")|" + sbX.ToString() + "|" + sb.ToString();
        }

        /// <summary>
        /// Find the index of the specified import filter from loaded importers
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public int ImportFilterIndex(string extension)
        {
            int count = -1;
            foreach (KeyValuePair<string, Type> kvp in LoadedActions)
            {
                count++;
                Type t = kvp.Value;
                if (typeof(IImportAction).IsAssignableFrom(t))
                {
                    ImportActionAttribute importAtt = ImportActionAttribute.ExtractFrom(t);
                    if (importAtt != null)
                    {
                        count++;
                        foreach (string ext in importAtt.Extensions)
                        {
                            if (extension.EqualsIgnoreCase(ext)) return count + 1; // +1 for 'all compatible types'
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Get a list of all currently loaded commands
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCommandList()
        {
            return LoadedActions.Keys.ToList();
        }
    }
    
}
