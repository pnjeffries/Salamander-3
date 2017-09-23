using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Base;
using Nucleus.ETABS;
using Nucleus.Model;

namespace Salamander.RobotIOPlugin
{
    [Action("SaveAsETABS",
        Description = "Export the Salamander model to CSI ETABS via the COM interface.  Requires ETABS to be installed.",
        IconBackground = Resources.URIs.Robot)]
    [ExportAction(Extensions = new string[] { ".edb" }, Filter = "ETABS File (*.edb) | *.edb")]
    public class SaveAsETABS : ModelDocumentActionBase, IExportAction
    {
        [ActionInput(1, "write toggle.  Set to true to write out a Robot file", Manual = false)]
        public bool Write { get; set; } = true;

        [ActionFilePathInput(Order = 2,
            Description = "the filepath to write to",
            Filter = "ETABS File (*.edb) | *.edb",
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
            Result = false;
            if (FilePath.IsValid && Write)
            {
                Document.Model.GenerateNodes(new NodeGenerationParameters());
                var etabs = new ETABSClient();
                etabs.Message += HandleMessage;
                ETABSIDMappingTable idMap = new ETABSIDMappingTable();
                etabs.WriteModelToEtabs(FilePath, Document.Model, ref idMap, new ETABSConversionOptions());
                etabs.Close();
                etabs.Release();
                Document.IDMappings[FilePath] = idMap;
                Result = true;
            }
            return true;
        }
    }
}

