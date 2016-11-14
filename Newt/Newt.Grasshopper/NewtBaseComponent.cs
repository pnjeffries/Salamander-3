using FreeBuild.Actions;
using FreeBuild.Extensions;
using FreeBuild.Geometry;
using FreeBuild.Rhino;
using Grasshopper.Kernel;
using Salamander.Actions;
using Salamander.RhinoCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using RC = Rhino.Geometry;

namespace Salamander.Grasshopper
{
    public abstract class NewtBaseComponent : GH_Component
    {
        #region Constants

        /// <summary>
        /// The name of the tab on which components are, by default, to be placed
        /// </summary>
        public const string CategoryName = "Newt";

        #endregion

        #region Properties

        /// <summary>
        /// The name of the command executed by this component
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// The type of action wrapped by this component
        /// </summary>
        public Type ActionType { get; private set; }

        /// <summary>
        /// The last action successfully executed by this component
        /// </summary>
        private IAction LastExecuted { get; set; }

        /// <summary>
        /// The execution information for the last execution
        /// </summary>
        private ExecutionInfo LastExecutionInfo { get; set; }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override Bitmap Internal_Icon_24x24
        {
            get
            {
                var actionAtt = ActionAttribute.ExtractFrom(ActionType);
                if (!string.IsNullOrWhiteSpace(actionAtt.IconURI))
                {
                    return IconResourceHelper.BitmapFromURI(actionAtt.IconURI);
                }
                return base.Internal_Icon_24x24;
            }
        }

        #endregion

        #region Constructors

        ///// <summary>
        ///// Constructor.  Call this from the default constructor of all subclasses, passing in the required information.
        ///// </summary>
        ///// <param name="commandName">The command name of the action that this component invokes</param>
        ///// <param name="name">The name of this component.  Keep it simple, single words are best.</param>
        ///// <param name="nickname">The abbreviation of this component.  Keep it short, 1-5 characters are best</param>
        ///// <param name="description">The description of this component.  Be succinct but clear.  You can supply whole sentances.</param>
        ///// <param name="category">The category of this component.  Controls which tab components will end up in.</param>
        ///// <param name="subCategory">The subcategory for this component.  Controls which group the component will be in.</param>
        //protected NewtBaseComponent(string commandName, string name, string nickname, string description,
        //    string subCategory, string category = CategoryName)
        //  : base()
        //{
        //    CommandName = commandName;
        //    Host.EnsureInitialisation();
        //    ActionType = Core.Instance.Actions.GetActionDefinition(CommandName);
        //    Name = name;
        //    NickName = nickname;
        //    Description = description;
        //    Category = category;
        //    SubCategory = subCategory;
        //    PostConstructor();
        //}

        /// <summary>
        /// Constructor.  Call this from the default constructor of all subclasses, passing in the required information.
        /// </summary>
        /// <param name="commandName">The command name of the action that this component invokes</param>
        /// <param name="name">The name of this component.  Keep it simple, single words are best.</param>
        /// <param name="nickname">The abbreviation of this component.  Keep it short, 1-5 characters are best</param>
        /// <param name="category">The category of this component.  Controls which tab components will end up in.</param>
        /// <param name="subCategory">The subcategory for this component.  Controls which group the component will be in.</param>
        protected NewtBaseComponent(string commandName, string name, string nickname,
            string subCategory, string category = CategoryName)
          : base()
        {
            CommandName = commandName;
            Host.EnsureInitialisation();
            ActionType = Core.Instance.Actions.GetActionDefinition(CommandName);
            var attributes = ActionAttribute.ExtractFrom(ActionType);
            Name = name;
            NickName = nickname;
            Description = attributes.Description.CapitaliseFirst();
            Category = category;
            SubCategory = subCategory;
            PostConstructor();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            NicknameConverter nC = new NicknameConverter();
            IList<PropertyInfo> inputs = ActionBase.ExtractInputParameters(ActionType);
            foreach (PropertyInfo pInfo in inputs)
            {
                Type inputType = pInfo.PropertyType;
                ActionInputAttribute inputAtt = ActionInputAttribute.ExtractFrom(pInfo);
                if (inputAtt != null)
                {
                    string name = pInfo.Name;
                    string nickname = string.IsNullOrEmpty(inputAtt.ShortName) ? nC.Convert(pInfo.Name) : inputAtt.ShortName;
                    string description = inputAtt.CapitalisedDescription;
                    if (inputType == typeof(double))
                    {
                        pManager.AddNumberParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (inputType == typeof(Vector))
                    {
                        pManager.AddPointParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (inputType == typeof(Line))
                    {
                        pManager.AddLineParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (typeof(Curve).IsAssignableFrom(inputType))
                    {
                        pManager.AddCurveParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else
                    {
                        pManager.AddGenericParameter(pInfo.Name, nickname, description, GH_ParamAccess.item);
                    }
                    //TODO
                }
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            NicknameConverter nC = new NicknameConverter();
            IList<PropertyInfo> outputs = ActionBase.ExtractOutputParameters(ActionType);
            foreach (PropertyInfo pInfo in outputs)
            {
                Type outputType = pInfo.PropertyType;
                ActionOutputAttribute outputAtt = ActionOutputAttribute.ExtractFrom(pInfo);
                if (outputAtt != null)
                {
                    string name = pInfo.Name;
                    string nickname = string.IsNullOrEmpty(outputAtt.ShortName) ? nC.Convert(pInfo.Name) : outputAtt.ShortName;
                    string description = outputAtt.CapitalisedDescription;
                    if (outputType == typeof(double))
                    {
                        pManager.AddNumberParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (outputType == typeof(Vector))
                    {
                        pManager.AddPointParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else
                    {
                        pManager.AddGenericParameter(pInfo.Name, nC.Convert(pInfo.Name), outputAtt.CapitalisedDescription, GH_ParamAccess.item);
                    }
                    //TODO
                }
            }
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            LastExecuted = null;
            if (ActionType != null)
            {
                IAction action = (IAction)Activator.CreateInstance(ActionType, true);
                if (action != null)
                {
                    if (ExtractInputs(action, DA))
                    {
                        ExecutionInfo exInfo = new ExecutionInfo(InstanceGuid.ToString(), DA.Iteration);
                        if (action.PreExecutionOperations(exInfo))
                            if (action.Execute(exInfo))
                                if (action.PostExecutionOperations(exInfo))
                                {
                                    ExtractOutputs(action, DA);
                                    LastExecuted = action;
                                    LastExecutionInfo = exInfo;
                                }
                    }
                }
            }
        }

        protected override void AfterSolveInstance()
        {
            base.AfterSolveInstance();
            //Delete unupdated objects, etc:
            if (LastExecuted != null)
            {
                LastExecuted.FinalOperations(LastExecutionInfo);
            }
        }

        /// <summary>
        /// Automatically populate action inputs using data extracted from the Grasshopper Data Access object.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="DA"></param>
        protected bool ExtractInputs(IAction action, IGH_DataAccess DA)
        {
            IList<PropertyInfo> inputs = ActionBase.ExtractInputParameters(ActionType);
            foreach (PropertyInfo pInfo in inputs)
            {
                Type inputType = pInfo.PropertyType;
                object inputData = GetInputData(pInfo.Name, inputType, DA);
                ActionInputAttribute inputAtt = ActionInputAttribute.ExtractFrom(pInfo);
                if (!inputAtt.Required || inputData != null)
                    pInfo.SetValue(action, inputData, null);
                else return false;
            }
            return true;
        }

        protected bool ExtractOutputs(IAction action, IGH_DataAccess DA)
        {
            IList<PropertyInfo> outputs = ActionBase.ExtractOutputParameters(ActionType);
            foreach (PropertyInfo pInfo in outputs)
            {
                object outputData = pInfo.GetValue(action, null);
                outputData = FormatForOutput(outputData);
                DA.SetData(pInfo.Name, outputData);
            }
            return true;
        }

        /// <summary>
        /// Extract data from the Grasshopper Data Access object, converting it into the specified type where necessary
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="DA"></param>
        /// <returns></returns>
        protected object GetInputData(string name, Type type, IGH_DataAccess DA)
        {
            object result = null;
            try
            {
                Type equivalentType = GetEquivalentType(type);
                MemberInfo[] mInfos = typeof(IGH_DataAccess).GetMember("GetData");
                MethodInfo getDataMethod = null;
                foreach (MethodInfo mInfo in mInfos)
                {
                    Type firstPT = mInfo.GetParameters()[0].ParameterType;
                    if (firstPT == typeof(string)) getDataMethod = mInfo;
                }
                if (getDataMethod != null)
                {
                    object[] args = new object[] { name, result };
                    MethodInfo getDataGeneric = getDataMethod.MakeGenericMethod(new Type[] { equivalentType });
                    bool success = (bool)getDataGeneric.Invoke(DA, args);
                    if (success)
                    {
                        result = args[1];
                        if (result.GetType() != type)
                        {
                            result = Convert(result, type);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, e.Message);
            }
            return result;
        }

        /// <summary>
        /// Wrap an object in goo, if possible
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected object FormatForOutput(object obj)
        {
            if (obj is Vector) return FBtoRC.Convert((Vector)obj);
            //TODO
            return obj;
        }

        /// <summary>
        /// Convert the specified object to the specified type.
        /// Override this function to convert to data types not natively supported
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="toType"></param>
        /// <returns></returns>
        /// <remarks>Currently supported:
        ///  DesignLink.Types.Line</remarks>
        protected virtual object Convert(object obj, Type toType)
        {
            return Conversion.Instance.Convert(obj, toType);
            /*
            //From RhinoCommon:
            if (toType.IsAssignableFrom(typeof(Curve)))
            {
                if (obj is RC.Curve) return RCtoFB.Convert((RC.Curve)obj);
            }
            //To RhinoCommon:
            if (toType.IsAssignableFrom(typeof(RC.Point3d)))
            {
                if (obj is Vector) return FBtoRC.Convert((Vector)obj);
            }
            return obj;
            */
        }

        protected virtual Type GetEquivalentType(Type type)
        {
            //TODO
            if (typeof(Line).IsAssignableFrom(type)) return typeof(RC.Line);
            else if (typeof(Curve).IsAssignableFrom(type)) return typeof(RC.Curve);
            return type;
        }

        #endregion

    }
}
