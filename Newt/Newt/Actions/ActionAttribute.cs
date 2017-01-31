using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Attribute c
    /// </summary>
    public class ActionAttribute : Attribute
    {
        /// <summary>
        /// The command string used to manually call this action
        /// </summary>
        public string CommandName { get; set; } = null;

        /// <summary>
        /// The description of this command
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// The URI string that identifies the image resource to be used as the foreground of the icon for this action 
        /// on buttons and grasshopper components.
        /// </summary>
        public string IconForeground { get; set; } = null;

        /// <summary>
        /// The URI string that identifies the image resource to be used as the background of the icon for this action
        /// on button and grasshopper components.
        /// </summary>
        public string IconBackground { get; set; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandName"></param>
        public ActionAttribute(string commandName)
        {
            CommandName = commandName;
        }

        /// <summary>
        /// CommandName, Description constructor
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="description"></param>
        public ActionAttribute(string commandName, string description) : this(commandName)
        {
            Description = description;
        }

        /// <summary>
        /// CommandName, Description, IconURI constructor
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="description"></param>
        /// <param name="iconURI"></param>
        public ActionAttribute(string commandName, string description, string iconURI) : this(commandName, description)
        {
            IconForeground = iconURI;
        }

        /// <summary>
        /// Helper function to get the (first) ActionAttribute from the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ActionAttribute ExtractFrom(Type type)
        {
            object[] actionAtts = type.GetCustomAttributes(typeof(ActionAttribute), false);
            if (actionAtts.Count() > 0)
            {
                return (ActionAttribute)actionAtts[0];
            }
            return null;
        }

        /// <summary>
        /// Helper function to get the action attributes from an action
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ActionAttribute ExtractFrom(IAction action)
        {
            if (action == null) return null;
            return ExtractFrom(action.GetType());
        }
    }
}
