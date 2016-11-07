using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Actions
{
    /// <summary>
    /// A base class for actions which perform operations on a ModelDocument
    /// </summary>
    public abstract class ModelDocumentActionBase : ActionBase
    {
        /// <summary>
        /// Private member variable for the Document property
        /// </summary>
        private ModelDocument _Document = null;

        /// <summary>
        /// The model document to be operated upon.  This can be specifically set, but by default will
        /// automatically return the currently active document.
        /// </summary>
        //[ActionInput(Description = "the document to be operated upon.", Manual = false, Required = false, Order =-10)]
        public ModelDocument Document
        {
            get
            {
                if (_Document == null) return Core.Instance.ActiveDocument;
                else return _Document;
            }
            set
            {
                _Document = value;
            }
        }

        public override bool PreExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (!base.PreExecutionOperations(exInfo)) return false;
            return true;
        }

        public override bool PostExecutionOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo != null) Document.Model.History.CleanIteration(exInfo);
            return true;
        }

        public override bool FinalOperations(ExecutionInfo exInfo = null)
        {
            if (exInfo != null) Document.Model.History.CleanSubsequentIterations(exInfo);
            return true;
        }
    }
}
