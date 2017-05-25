using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Interface for actions which perform operations on a particular ModelDocument.
    /// Implement this interface for any actions which may potentially need to operate on
    /// Documents which are not the active one - for example Grasshopper components that
    /// may need to be able to write to a background model.
    /// </summary>
    public interface IModelDocumentAction : IAction
    {
        /// <summary>
        /// The model document to be operated upon.  This can be specifically set, but by default will
        /// automatically return the currently active document.
        /// </summary>
        ModelDocument Document { get; set; }
    }
}
