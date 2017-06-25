using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Interface for actions which import model documents from an external file
    /// </summary>
    public interface IImportDocumentAction : IImportAction
    {
        /// <summary>
        /// The output document imported from a file
        /// </summary>
        ModelDocument Document { get; set; }
    }
}
