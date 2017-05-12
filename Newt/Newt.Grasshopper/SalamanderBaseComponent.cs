using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Extensions;
using FreeBuild.Geometry;
using FreeBuild.Model;
using FreeBuild.Rhino;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Salamander.Actions;
using Salamander.Display;
using Salamander.Rhino;
using Salamander.RhinoCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using RC = Rhino.Geometry;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Base class for Salamander 3 components
    /// </summary>
    public abstract class SalamanderBaseComponent : GH_Component
    {
        #region 

        /// <summary>
        /// The name of the tab on which components are, by default, to be placed
        /// </summary>
        public const string CategoryName = "Salamander 3";

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
                if (!string.IsNullOrWhiteSpace(actionAtt.IconForeground))
                {
                    if (!string.IsNullOrWhiteSpace(actionAtt.IconBackground))
                    {
                        return IconResourceHelper.CombinedBitmapFromURIs(actionAtt.IconBackground, actionAtt.IconForeground);
                    }
                    else return IconResourceHelper.BitmapFromURI(actionAtt.IconForeground);
                }
                else if (!string.IsNullOrWhiteSpace(actionAtt.IconBackground)) return IconResourceHelper.BitmapFromURI(actionAtt.IconBackground);
                return base.Internal_Icon_24x24;
            }
        }

        /// <summary>
        /// The display layer used to draw the preview for this component
        /// </summary>
        protected DisplayLayer PreviewLayer { get; set; } = null;


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
        protected SalamanderBaseComponent(string commandName, string name, string nickname,
            string subCategory, string category = CategoryName)
          : base()
        {
            CommandName = commandName;
            Host.EnsureInitialisation();
            ActionType = Core.Instance.Actions.GetActionDefinition(CommandName);
            if (ActionType == null) throw new Exception("Command '" + CommandName + "' has not been found.  The plugin that contains it may not have been successfully loaded.");
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
        /// Get the appropriate GH_ParamAccess value for a collection type with the given ActionInput attribute
        /// </summary>
        /// <param name="inputAtt"></param>
        /// <returns></returns>
        private GH_ParamAccess ParamAccess(ActionInputAttribute inputAtt)
        {
            if (inputAtt.OneByOne) return GH_ParamAccess.item;
            else return GH_ParamAccess.list;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            IAction action = (IAction)Activator.CreateInstance(ActionType, true);
            NicknameConverter nC = new NicknameConverter();
            IList<PropertyInfo> inputs = ActionBase.ExtractInputParameters(ActionType);
            foreach (PropertyInfo pInfo in inputs)
            {
                Type pType = pInfo.PropertyType;
                ActionInputAttribute inputAtt = ActionInputAttribute.ExtractFrom(pInfo);
                if (inputAtt != null)
                {
                    string name = pInfo.Name;
                    string nickname = string.IsNullOrEmpty(inputAtt.ShortName) ? nC.Convert(pInfo.Name) : inputAtt.ShortName;
                    string description = inputAtt.CapitalisedDescription;
                    if (pType == typeof(double))
                    {
                        pManager.AddNumberParameter(name, nickname, description, GH_ParamAccess.item, (double)pInfo.GetValue(action));
                    }
                    else if (pType == typeof(string))
                    {
                        pManager.AddTextParameter(name, nickname, description, GH_ParamAccess.item, (string)pInfo.GetValue(action));
                    }
                    else if (pType == typeof(bool))
                    {
                        pManager.AddBooleanParameter(name, nickname, description, GH_ParamAccess.item, (bool)pInfo.GetValue(action));
                    }
                    else if (pType == typeof(Vector))
                    {
                        pManager.AddPointParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(Line))
                    {
                        pManager.AddLineParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(Angle))
                    {
                        pManager.AddAngleParameter(name, nickname, description, GH_ParamAccess.item, (Angle)pInfo.GetValue(action));
                    }
                    else if (typeof(Curve).IsAssignableFrom(pType))
                    {
                        pManager.AddCurveParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (typeof(LinearElement).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new LinearElementParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (typeof(Element).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new LinearElementParam(); //TEMP!
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (typeof(LinearElementCollection).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new LinearElementParam();
                        pManager.AddParameter(param, name, nickname, description, ParamAccess(inputAtt));
                    }
                    else if (typeof(ElementCollection).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new LinearElementParam(); //TEMP!
                        pManager.AddParameter(param, name, nickname, description, ParamAccess(inputAtt));
                    }
                    else if (pType == typeof(Node))
                    {
                        IGH_Param param = new NodeParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(NodeCollection))
                    {
                        IGH_Param param = new NodeParam();
                        pManager.AddParameter(param, name, nickname, description, ParamAccess(inputAtt));
                    }
                    else if (typeof(SectionFamily).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new SectionFamilyParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(Bool6D))
                    {
                        IGH_Param param = new Bool6DParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(ActionTriggerInput))
                    {
                        pManager.AddGenericParameter(name, nickname, description, GH_ParamAccess.tree);
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
                Type pType = pInfo.PropertyType;
                ActionOutputAttribute outputAtt = ActionOutputAttribute.ExtractFrom(pInfo);
                if (outputAtt != null)
                {
                    string name = pInfo.Name;
                    string nickname = string.IsNullOrEmpty(outputAtt.ShortName) ? nC.Convert(pInfo.Name) : outputAtt.ShortName;
                    string description = outputAtt.CapitalisedDescription;
                    if (pType == typeof(double) || pType == typeof(Angle))
                    {
                        pManager.AddNumberParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(string))
                    {
                        pManager.AddTextParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(bool))
                    {
                        pManager.AddBooleanParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(Vector))
                    {
                        pManager.AddPointParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (typeof(Curve).IsAssignableFrom(pType))
                    {
                        pManager.AddCurveParameter(name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(LinearElement))
                    {
                        IGH_Param param = new LinearElementParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(LinearElementCollection))
                    {
                        IGH_Param param = new LinearElementParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.list);
                    }
                    else if (pType == typeof(Element))
                    {
                        IGH_Param param = new LinearElementParam(); //TEMP!
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(ElementCollection))
                    {
                        IGH_Param param = new LinearElementParam(); //TEMP!
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.list);
                    }
                    else if (pType == typeof(Node))
                    {
                        IGH_Param param = new NodeParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(NodeCollection))
                    {
                        IGH_Param param = new NodeParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.list);
                    }
                    else if (typeof(SectionFamily).IsAssignableFrom(pType))
                    {
                        IGH_Param param = new SectionFamilyParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else if (pType == typeof(Bool6D))
                    {
                        IGH_Param param = new Bool6DParam();
                        pManager.AddParameter(param, name, nickname, description, GH_ParamAccess.item);
                    }
                    else
                    {
                        pManager.AddGenericParameter(pInfo.Name, nC.Convert(pInfo.Name), outputAtt.CapitalisedDescription, GH_ParamAccess.item);
                    }
                    //TODO
                }
            }
        }

        protected override void BeforeSolveInstance()
        {
            if (PreviewLayer != null) PreviewLayer.Clear();
            base.BeforeSolveInstance();
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
                if (PreviewLayer != null) PreviewLayer.TryRegister(outputData);
                outputData = FormatForOutput(outputData);
                if (outputData is IList)
                {
                    DA.SetDataList(pInfo.Name, (IEnumerable)outputData);
                }
                else DA.SetData(pInfo.Name, outputData);

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
            if (type != typeof(ActionTriggerInput))
            {
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
            else if (obj is Curve) return FBtoRC.Convert((Curve)obj);
            else if (obj is LinearElement) return new LinearElementGoo((LinearElement)obj);
            else if (obj is LinearElementCollection) return LinearElementGoo.Convert((LinearElementCollection)obj);
            else if (obj is PanelElement) return new PanelElementGoo((PanelElement)obj);
            else if (obj is PanelElementCollection) return PanelElementGoo.Convert((PanelElementCollection)obj);
            else if (obj is SectionFamily) return new SectionFamilyGoo((SectionFamily)obj);
            else if (obj is BuildUpFamily) return new BuildUpFamilyGoo((BuildUpFamily)obj);
            else if (obj is Node) return new NodeGoo((Node)obj);
            else if (obj is NodeCollection) return NodeGoo.Convert((NodeCollection)obj);
            else if (obj is Bool6D) return new Bool6DGoo((Bool6D)obj);
            else if (obj is FilePath) return obj.ToString();
            //Add more types here
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
            //if (obj is SectionFamilyGoo && typeof(SectionFamily).IsAssignableFrom(toType)) return ((SectionFamilyGoo)obj).Value;
            //if (toType == typeof(LinearElementGoo)) return new LinearElementGoo(obj as LinearElement);
            //else if (toType == typeof(SectionFamily)) return new SectionFamilyGoo(obj as SectionFamily);

            if (obj is ISalamander_Goo) return ((ISalamander_Goo)obj).GetValue(toType);
            else if (toType == typeof(Angle)) return new Angle((double)obj);
            else if (toType == typeof(FilePath)) return new FilePath(obj.ToString());
            else if (toType == typeof(ActionTriggerInput)) return new ActionTriggerInput();

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
            if (typeof(Line).IsAssignableFrom(type)) return typeof(RC.Line);
            else if (type == typeof(Vector)) return typeof(RC.Point3d);
            else if (type == typeof(Angle)) return typeof(double);
            else if (typeof(Curve).IsAssignableFrom(type)) return typeof(RC.Curve);
            else if (typeof(LinearElement).IsAssignableFrom(type)) return typeof(LinearElementGoo);
            else if (typeof(PanelElement).IsAssignableFrom(type)) return typeof(PanelElementGoo);
            else if (typeof(Element).IsAssignableFrom(type)) return typeof(LinearElementGoo); // TEMP!
            else if (typeof(LinearElementCollection).IsAssignableFrom(type)) return typeof(LinearElementGoo);
            else if (typeof(PanelElementCollection).IsAssignableFrom(type)) return typeof(PanelElementGoo);
            else if (typeof(ElementCollection).IsAssignableFrom(type)) return typeof(LinearElementGoo);
            else if (typeof(SectionFamily).IsAssignableFrom(type)) return typeof(SectionFamilyGoo);
            else if (typeof(BuildUpFamily).IsAssignableFrom(type)) return typeof(BuildUpFamilyGoo);
            else if (typeof(Node).IsAssignableFrom(type)) return typeof(NodeGoo);
            else if (typeof(NodeCollection).IsAssignableFrom(type)) return typeof(NodeGoo);
            else if (typeof(Bool6D).IsAssignableFrom(type)) return typeof(Bool6DGoo);
            else if (typeof(FilePath) == type) return typeof(string);
            else if (typeof(ActionTriggerInput) == type) return typeof(object);
            return type;
        }

        //public override void DrawViewportMeshes(IGH_PreviewArgs args)
        //{
        //    if (PreviewLayer != null)
        //    {
        //        PreviewLayer.Draw(new RhinoRenderingParameters(args.Display));
        //    }
        //    base.DrawViewportMeshes(args);
        //}

        #endregion

    }
}