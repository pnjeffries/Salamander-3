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
    [Action("SaveAsRobot")]
    [ExportAction(Extensions = new string[] { ".rtd" }, Filter = "Robot File (*.rtd) | *.rtd")]
    public class RobotExportAction : ModelDocumentActionBase, IExportAction
    {
        //[ActionFilePathInput(Order = 0,
        //    Description = "the filepath to write to",
        //    Filter = "Robot File (*.rtd) | *.rtd")]
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Document.Model.RegenerateNodes(new NodeGenerationParameters());
            var robot = new RobotController();
            robot.Message += HandleMessage;
            RobotIDMappingTable idMap = new RobotIDMappingTable();
            robot.WriteModelToRobot(FilePath, Document.Model, ref idMap);
            robot.Close();
            robot.Release();
            return true;
        }
    }
}
