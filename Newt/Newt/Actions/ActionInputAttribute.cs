using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Attributes of an action input parameter property.
    /// Used to specify information about the input and how it should be obtained/used.
    /// Supported
    /// </summary>
    public class ActionInputAttribute : ActionParameterAttribute
    {
        /// <summary>
        /// If true, the input will be prompted for as an input when this action is run as a manual command.
        /// Default = true
        /// </summary>
        public bool Manual { get; set; } = true;

        /// <summary>
        /// If true, this input *must* be set to a non-null value or the command will not be executed.
        /// Default = true
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// If true, the value specified for this input will be re-used as the default value the next time the command is run.
        /// Default = false
        /// </summary>
        public bool Persistant { get; set; } = false;

        /// <summary>
        /// If true (and the data is of an importable type) the data is to be loaded from an external file rather than selected from the current document
        /// An import dialog of the appropriate type will be displayed
        /// Default = false
        /// </summary>
        public bool Import { get; set; } = false;

        /// <summary>
        /// If true, this input will be set via an auto-generated options dialog
        /// Default = false
        /// </summary>
        public bool Dialog { get; set; } = false;

        /// <summary>
        /// Only applies to collection types when being wrapped inside a paramatric component.
        /// If true (default) objects in this collection will be processed one at a time and
        /// the action executed once for each item individually.
        /// </summary>
        public bool OneByOne { get; set; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionInputAttribute()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="order"></param>
        /// <param name="description"></param>
        public ActionInputAttribute(int order, string description)
        {
            Order = order;
            Description = description;
        }

        /// <summary>
        /// Helper function to get the (first) ActionInput attribute from the specified PropertyInfo
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public static ActionInputAttribute ExtractFrom(PropertyInfo pInfo)
        {
            object[] actionAtts = pInfo.GetCustomAttributes(typeof(ActionInputAttribute), false);
            if (actionAtts.Count() > 0)
            {
                return (ActionInputAttribute)actionAtts[0];
            }
            return null;
        }
    }
}
