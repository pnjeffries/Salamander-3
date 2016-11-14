using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// A base class for actions which act on a Model.
    /// </summary>
    public abstract class ModelActionBase : ActionBase
    {
        /// <summary>
        /// Private member variable for Model
        /// </summary>
        private Model _Model = null;

        /// <summary>
        /// The model to be operated upon.  This can be specifically set, but by default will
        /// automatically return the currently active document's model.
        /// </summary>
        //[ActionInput(Description = "the document to be operated upon.", Manual = false, Required = false, Order =-10)]
        public Model Model
        {
            get
            {
                if (_Model == null) return Core.Instance.ActiveDocument.Model;
                else return _Model;
            }
            set
            {
                _Model = value;
            }
        }

        public override bool PreExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (!base.PreExecutionOperations(exInfo)) return false;
            return true;
        }

        public override bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo != null) Model.History.CleanIteration(exInfo);
            return true;
        }

        public override bool FinalOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo != null) Model.History.CleanSubsequentIterations(exInfo);
            return true;
        }
    }
}
