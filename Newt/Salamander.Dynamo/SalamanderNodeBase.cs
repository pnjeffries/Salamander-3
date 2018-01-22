using Dynamo.Graph.Nodes;
using Nucleus.Actions;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Dynamo
{
    /// <summary>
    /// An abstract base class for Salamander nodes in Dynamo
    /// </summary>
    public abstract class SalamanderNodeBase : NodeModel
    {
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


        public override string CreationName
        {
            get { return CommandName; }
        }

        #endregion

        #region Constructors

        public SalamanderNodeBase(string commandName)
        {
            CommandName = commandName;
        }

        #endregion

        #region Methods



        #endregion

    }
}
