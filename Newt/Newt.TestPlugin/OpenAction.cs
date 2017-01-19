using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    [Action("Open")]
    [ImportAction(Extensions = new string[] { ".sal" }, Filter = "Salamander 3 File (*.sal) | *.sal")]
    public class OpenAction : ModelDocumentActionBase, IImportDocumentAction
    {
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Document = ModelDocument.Load(FilePath);
            return true;
        }
    }
}
