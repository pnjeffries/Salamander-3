using FreeBuild.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// Base class for actions which import a design from a file
    /// </summary>
    public abstract class ImportActionBase : ActionBase, IImportAction
    {
        /// <summary>
        /// The filepath the exporter will write to
        /// </summary>
        [ActionFilePathInput(
            Description = "the file to open.",
            Manual = false,
            Order = -9,
            Open = true)]
        public FilePath FilePath { get; set; }

    }
}
