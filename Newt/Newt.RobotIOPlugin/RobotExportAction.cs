using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Robot;
using FreeBuild.Model;

namespace Salamander.RobotIOPlugin
{
    [Action("SaveAsRobot", 
        Description = "Export the Salamander model to Autodesk Robot via the COM interface.  Requires Robot to be installed.",
        IconBackground = Resources.URIs.Robot)]
    [ExportAction(Extensions = new string[] { ".rtd" }, Filter = "Robot File (*.rtd) | *.rtd")]
    public class RobotExportAction : ModelDocumentActionBase, IExportAction
    {
        [ActionInput(1, "write toggle.  Set to true to write out a Robot file", Manual = false)]
        public bool Write { get; set; } = true;

        [ActionFilePathInput(Order = 2,
            Description = "the filepath to write to",
            Filter = "Robot File (*.rtd) | *.rtd",
            Manual = false)]
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
                var robot = new RobotController();
                robot.Message += HandleMessage;
                RobotIDMappingTable idMap = new RobotIDMappingTable();
                robot.WriteModelToRobot(FilePath, Document.Model, ref idMap);
                robot.Close();
                robot.Release();
                Document.IDMappings[FilePath] = idMap;
                Result = true;
            }
            return true;
        }
    }
}
