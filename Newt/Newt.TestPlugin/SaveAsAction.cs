using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Base;

namespace Salamander.BasicTools
{
    [Action("SaveAs",
        Description = "Save the model as a Salamander .s3b file",
        IconBackground = Resources.URIs.Salamander)]
    [ExportAction(Extensions = new string[] { ".s3b" }, Filter = "Salamander 3 File (*.s3b) | *.s3b")]
    public class SaveAsAction : ModelDocumentActionBase, IExportAction
    {
        [ActionInput(1, "write toggle.  Set to true to write out a Salamander file", Manual = false)]
        public bool Write { get; set; } = true;

        [ActionFilePathInput(Order = 2,
            Description = "the filepath to write to",
            Filter = "Salamander 3 File (*.s3b) | *.s3b",
            Manual = true)]
        public FilePath FilePath { get; set; }

        [ActionInput(3,
           "trigger input.  This input stream will not be used directly, but exists to allow file export to be automatically triggered on update of this data stream.",
           Manual = false, Required = false)]
        public ActionTriggerInput Trigger { get; set; }

        [ActionOutput(1, "whether the file was successfully written")]
        public bool Result { get; set; } = false;

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            if (Write)
            {
                Print("Saving file... ");
                Result = Document.SaveAs(FilePath);
                if (Result) PrintLine("Saved to '" + FilePath + "'");
                else PrintLine("Saving Failed!");
            }
            return true;
        }
    }
}
