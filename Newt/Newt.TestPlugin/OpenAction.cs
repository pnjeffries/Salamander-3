using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Base;
using Nucleus.Model;

namespace Salamander.BasicTools
{
    [Action("Open")]
    [ImportAction(Extensions = new string[] { ".s3b" }, Filter = "Salamander 3 File (*.s3b) | *.s3b")]
    public class OpenAction : ModelActionBase, IImportDocumentAction
    {
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Document = ModelDocument.Load(FilePath);
            return true;
        }
    }
}
