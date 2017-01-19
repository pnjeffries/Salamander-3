using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Base;

namespace Salamander.BasicTools
{
    [Action("SaveAs")]
    [ExportAction(Extensions = new string[] { ".sal" }, Filter = "Salamander 3 File (*.sal) | *.sal")]
    public class SaveAsAction : ModelDocumentActionBase, IExportAction
    {
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Document.SaveAs(FilePath);
            return true;
        }
    }
}
