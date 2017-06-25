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
    [Action("SaveAsGWA", 
        Description = "Export the Salamander model to a text GWA format for import to Oasys GSA",
        IconBackground = Resources.URIs.GSA)]
    [ExportAction(Extensions = new string[] { ".gwa" }, Filter = "GSA Text File (*.gwa) | *.gwa")]
    public class GWAExportAction : ModelDocumentActionBase, IExportAction
    {
        [ActionInput(1, "write toggle.  Set to true to write out a GWA file", Manual = false)]
        public bool Write { get; set; } = true;

        [ActionInput(2, "the filePath to write to", Manual = false)] //TODO: Manual when not already set
        public FilePath FilePath { get; set; }

        [ActionInput(3, 
            "trigger input.  This input stream will not be used directly, but exists to allow file export to be automatically triggered on update of this data stream.",
            Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        [ActionOutput(1, "whether the file was successfully written")]
        public bool Result { get; set; } = false;
        
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Result = false;
            if (FilePath.IsValid && Write)
            {
                Document.Model.GenerateNodes(new NodeGenerationParameters());
                var serialiser = new ModelDocumentTextSerialiser(new GWAFormat(), new GWAContext());
                serialiser.CustomHeader = "! This file was originally written by Salamander 3 on " + DateTime.Now;
                Result = Document.SaveAs(FilePath, serialiser);
            }
            return true;
        }
    }
}
