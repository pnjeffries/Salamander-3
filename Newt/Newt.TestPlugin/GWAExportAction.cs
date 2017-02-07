using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Base;
using FreeBuild.Actions;
using FreeBuild.IO;
using FreeBuild.Model;

namespace Salamander.BasicTools
{
    [Action("SaveAsGWA")]
    [ExportAction(Extensions = new string[] { ".gwa" }, Filter = "GSA Text File (*.gwa) | *.gwa")]
    public class GWAExportAction : ModelDocumentActionBase, IExportAction
    {
        public FilePath FilePath { get; set; }
        
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Document.Model.GenerateNodes(new NodeGenerationParameters());
            var serialiser = new ModelDocumentTextSerialiser(new GWAFormat(), new GWAContext());
            Document.SaveAs(FilePath, serialiser);
            return true;
        }
    }
}
