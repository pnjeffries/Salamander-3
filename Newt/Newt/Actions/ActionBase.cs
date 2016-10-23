using FreeBuild.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Actions
{
    /// <summary>
    /// A base class for actions.  This provides some built-in functionality for the sake of convenience,
    /// however you can go fully custom if you want by implementing the IAction interface yourself.
    /// </summary>
    public abstract class ActionBase : IAction
    {
        /// <summary>
        /// Execute this action.  Input parameters will be consumed and output parameters will be populated.
        /// </summary>
        /// <param name="exInfo">A set of data communicating the parametric context of execution.
        /// Used to update existing outputs from the same parametric source instead of generating new ones.
        /// Will be null if the source is not part of a parametric process.</param>
        /// <returns>Should return true if the action is successfully completed, false otherwise</returns>
        public abstract bool Execute(ExecutionInfo exInfo = null);

        public IList<PropertyInfo> InputParameters()
        {
            return ExtractInputParameters(this.GetType());
        }

        public IList<PropertyInfo> OutputParameters()
        {
            return ExtractOutputParameters(this.GetType());
        }

        /// <summary>
        /// Use the current input manager to prompt the user for all necessary inputs to this action
        /// </summary>
        /// <returns>True if all inputs successful, false if user cancels</returns>
        public virtual bool PromptUserForInputs()
        {
            IList<PropertyInfo> inputs = InputParameters();
            IList<PropertyInfo> dialogOptions = null;
            foreach (PropertyInfo input in inputs)
            {
                object value = input.GetValue(this, null); //Initial value
                object[] attributes = input.GetCustomAttributes(typeof(ActionInputAttribute), true);
                ActionInputAttribute inputAtt = null;
                if (attributes.Count() > 0) inputAtt = (ActionInputAttribute)attributes[0];
                if (inputAtt != null && inputAtt.Manual)
                {
                    if (inputAtt.Dialog)
                    {
                        if (dialogOptions == null) dialogOptions = new List<PropertyInfo>();
                        dialogOptions.Add(input);
                    }
                    else if (!Core.Instance.Host.Input.EnterInput(input.PropertyType, ref value, input) && inputAtt.Required) return false;
                }
                input.SetValue(this, value, null);
            }
            if (dialogOptions != null)
            {
                //TODO!
                //if (!Core.Instance.Host.GUI.ShowGeneratedDialog(this, dialogOptions)) return false;
            }
            return true;
        }

        /// <summary>
        /// Print a message in the host application
        /// </summary>
        /// <param name="text"></param>
        protected void Print(string text)
        {
            Core.Instance.Host.Print(text);
        }

        /// <summary>
        /// Print a message in the host application followed by a newline character
        /// </summary>
        /// <param name="text"></param>
        protected void PrintLine(string text)
        {
            Core.Instance.Host.Print(text + "\n");
        }

        /// <summary>
        /// Print all 
        /// </summary>
        public void PrintOutputs()
        {
            IList<PropertyInfo> outputs = OutputParameters();
            foreach (PropertyInfo output in outputs)
            {
                object value = output.GetValue(this, null);
                PrintLine(output.Name + ": " + value.ToString());
            }
        }

        /// <summary>
        /// Helper function to extract all properties from the given type with the specified type of
        /// ActionParameter annotation.
        /// </summary>
        /// <param name="type">The type from which to extract parameters</param>
        /// <param name="parameterType">The subtype of ActionParameter annotation to be searched for</param>
        /// <returns></returns>
        public static IList<PropertyInfo> ExtractActionParameters(Type type, Type parameterType)
        {
            SortedList<double, PropertyInfo> result = new SortedList<double, PropertyInfo>();
            PropertyInfo[] pInfos = type.GetProperties();
            foreach (PropertyInfo pInfo in pInfos)
            {
                object[] attributes = pInfo.GetCustomAttributes(parameterType, true);
                if (attributes.Count() > 0)
                {
                    ActionParameterAttribute aInput = (ActionParameterAttribute)attributes[0];
                    double keyValue = aInput.Order;
                    while (result.ContainsKey(keyValue)) keyValue += 0.00001;
                    result.Add(keyValue, pInfo);
                }
            }
            return result.Values.ToList<PropertyInfo>();
        }

        /// <summary>
        /// Extract a list of all properties annotated as ActionInputs from the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> ExtractInputParameters(Type type)
        {
            return ExtractActionParameters(type, typeof(ActionInputAttribute));
        }

        /// <summary>
        /// Extract a list of all properties annotated as ActionOutputs from the specififed type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> ExtractOutputParameters(Type type)
        {
            return ExtractActionParameters(type, typeof(ActionOutputAttribute));
        }

        /// <summary>
        /// Base PostExecutionOperations implementation.  Override this to add operations to be performed after each execution.
        /// </summary>
        /// <param name="exInfo"></param>
        /// <returns></returns>
        public virtual bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            return true;
        }

        /// <summary>
        /// Base PreExecutionOperations implementation.  Override this to add operations to be performed prior to each execution.
        /// </summary>
        /// <param name="exInfo"></param>
        /// <returns></returns>
        public virtual bool PreExecutionOperations(ExecutionInfo exInfo = null)
        {
            return true;
        }

        /// <summary>
        /// A function which will be called on the last action to be executed in a sequence, after any
        /// PostExecutionOperations have been performed..  For example, in a Grasshopper component dealing 
        /// with multiple inputs, this will only be called once all inputs have been dealt with.
        /// Use this to define any tidying up operations that should be performed after the final iteration.
        /// </summary>
        /// <param name="exInfo"></param>
        /// <returns></returns>
        public virtual bool FinalOperations(ExecutionInfo exInfo = null)
        {
            return true;
        }
    }
}
