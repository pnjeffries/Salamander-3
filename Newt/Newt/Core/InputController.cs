using FreeBuild.Base;
using FreeBuild.Geometry;
using FreeBuild.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Salamander
{
    /// <summary>
    /// Abstract base class for Input Controllers.
    /// Input controllers are manager classes responsible for requesting information from
    /// the user in the host application.
    /// </summary>
    public abstract class InputController
    {

        #region Methods

        /// <summary>
        /// Prompt the user to enter a save filepath
        /// </summary>
        /// <param name="prompt">The prompt message text</param>
        /// <param name="filter">The file type filter string</param>
        /// <param name="defaultValue">The default value for the filepath</param>
        /// <returns>The filepath if one is selected, else null</returns>
        public FilePath EnterSaveFilePath(string prompt = "Enter save file location", 
            string filter = null, string defaultValue = null)
        {
            return Core.Instance.Host.GUI.ShowSaveFileDialog(prompt, filter, defaultValue);
        }

        /// <summary>
        /// Prompt the user to enter a filepath to open
        /// </summary>
        /// <param name="prompt">The prompt message text</param>
        /// <param name="filter">The file type filter string</param>
        /// <param name="defaultValue">The default value for the filepath</param>
        /// <returns>The filepath if one is selected, else null</returns>
        public FilePath EnterOpenFilePath(string prompt = "Select file to open", 
            string filter = null, string defaultValue = null)
        {
            return Core.Instance.Host.GUI.ShowOpenFileDialog(prompt, filter, defaultValue);
        }

        /// <summary>
        /// Prompt the user to enter an integer number
        /// </summary>
        /// <param name="prompt">The prompt message to be displayed</param>
        /// <param name="defaultValue">The default value which will be suggested to the user</param>
        /// <returns></returns>
        public abstract int EnterInteger(string prompt = "Enter integer value", int defaultValue = 0);

        /// <summary>
        /// Prompt the user to enter a number
        /// </summary>
        /// <param name="prompt">The prompt message to be displayed</param>
        /// <param name="defaultValue">The default value which will be suggested to the user</param>
        /// <returns>A double if the user enters one, else double.NaN if they cancel.</returns>
        public abstract double EnterDouble(string prompt = "Enter value", double defaultValue = 0);

        /// <summary>
        /// Prompt the user to enter a string
        /// </summary>
        /// <param name="prompt">The prompt message to be displayed</param>
        /// <param name="defaultValue">The default value which will be suggested to the user</param>
        /// <returns></returns>
        public abstract string EnterString(string prompt = "Enter string", string defaultValue = null);

        /// <summary>
        /// Prompt the user to select a point in 3D space
        /// </summary>
        /// <param name="prompt">The prompt message to be displayed</param>
        /// <returns>A point, or null if the user cancels</returns>
        public abstract Vector EnterPoint(string prompt = "Enter point");

        /// <summary>
        /// Prompt the user to enter a line by start and end point
        /// </summary>
        /// <param name="startPointPrompt">The prompt to be displayed when entering the first point</param>
        /// <param name="endPointPrompt">The prompt to be displayed when entering the second point</param>
        /// <param name="startPoint">A point which will be taken as the start of the line.  
        /// If non-null, the first point will not be asked for.</param>
        /// <returns></returns>
        public abstract Line EnterLine(string startPointPrompt = "Enter start of line", 
            string endPointPrompt = "Enter end of line", Vector? startPoint = null);

        /// <summary>
        /// Prompt the user to enter a collection of geometry
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public abstract VertexGeometryCollection EnterGeometry(string prompt = "Enter geometry");

        /// <summary>
        /// Prompt the user to select an Element
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public abstract Element EnterElement(string prompt = "Enter element");

        /// <summary>
        /// Prompt the user to select multiple elements
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public abstract ElementCollection EnterElements(string prompt = "Enter elements");

        /// <summary>
        /// Prompt the user to select a Linear Element
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public abstract LinearElement EnterLinearElement(string prompt = "Enter linear element");

        /// <summary>
        /// Prompt the user to select multiple Linear Element
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public abstract LinearElementCollection EnterLinearElements(string prompt = "Enter linear elements");

        /// <summary>
        /// Prompt the user to enter data of the specified type.
        /// The appropriate 'Enter____' function will be automatically selected based on the type.
        /// Override this to enable actions to use application-specific data types as inputs.
        /// </summary>
        /// <param name="inputType">The type of data to be entered</param>
        /// <param name="value">Input: The default value.  Output: The selected value./param>
        /// <param name="property">Optional.  The property being populated.</param>
        /// <param name="action">Optional.  The action the input parameters of which are currently 
        /// being populated.</param>
        public virtual bool EnterInput(Type inputType, ref object value, PropertyInfo property = null, 
            IAction action = null)
        {
            string description = null;
            ActionInputAttribute inputAttributes = ActionInputAttribute.ExtractFrom(property);
            if (inputAttributes != null)
            {
                description = inputAttributes.Description;
            }
            if (description == null) description = inputType.Name;

            try
            {
                if (inputType == typeof(double)) //Double
                    value = EnterDouble("Enter " + description, (double)value);
                else if (inputType == typeof(int)) //Integer
                    value = EnterInteger("Enter " + description, (int)value);
                else if (inputType == typeof(string)) //String
                    value = EnterString("Enter " + description, (string)value);
                else if (inputType == typeof(FilePath)) //FilePath
                {
                    string filter = "All Files  (*.*)|*.*";
                    bool open = false;
                    if (inputAttributes != null && inputAttributes is ActionFilePathInputAttribute)
                    {
                        ActionFilePathInputAttribute filePathAttribute = (ActionFilePathInputAttribute)inputAttributes;
                        filter = filePathAttribute.Filter;
                        open = filePathAttribute.Open;
                    }
                    if (open) value = EnterOpenFilePath("Select " + description, filter, (FilePath)value);
                    else value = EnterSaveFilePath("Select " + description, filter, (FilePath)value);
                }
                else if (inputType == typeof(Vector)) //FreeBuild Vector
                    value = EnterPoint("Enter " + description);
                else if (inputType == typeof(Line)) //FreeBuild Line
                    value = EnterLine("Enter start point of " + description,
                        "Enter end point of " + description);
                else if (inputType == typeof(VertexGeometryCollection)) //FreeBuild Shapes
                    value = EnterGeometry("Enter " + description);
                else if (inputType == typeof(Element)) // FreeBuild Element
                    value = EnterElement("Enter " + description);
                else if (inputType == typeof(ElementCollection)) // FreeBuild Elements
                    value = EnterElements("Enter " + description);
                else if (inputType == typeof(LinearElement))
                    value = EnterLinearElement("Enter " + description);
                else if (inputType == typeof(LinearElementCollection))
                    value = EnterLinearElements("Enter " + description);
                else return false; //Input not recognised
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
