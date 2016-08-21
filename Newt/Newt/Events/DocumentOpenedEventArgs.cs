using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Events
{
    /// <summary>
    /// Event arguments for when a document 
    /// </summary>
    public class DocumentOpenedEventArgs : EventArgs
    {
        /// <summary>
        /// The design document that has been opened
        /// </summary>
        public ModelDocument Document { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document"></param>
        public DocumentOpenedEventArgs(ModelDocument document)
        {
            Document = document;
        }
    }
}
