using Newt.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Actions;
using FreeBuild.Base;
using FreeBuild.Robot;
using FreeBuild.Model;

namespace Newt.RobotIOPlugin
{
    [Action("OpenRobot")]
    [ImportAction(Extensions = new string[] { ".rtd" }, Filter = "Robot File (*.rtd) | *.rtd")]
    public class RobotImportAction : ModelDocumentActionBase, IImportDocumentAction
    {
        [ActionFilePathInput(Order = 0,
            Description = "the Robot file to import",
            Open = true,
            Filter = "Robot File (*.rtd) | *.rtd")]
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var robot = new RobotController();
            RobotIDMappingTable idMap = new RobotIDMappingTable();
            Model model = robot.LoadModelFromRobot(FilePath, ref idMap);
            Document = new ModelDocument(FilePath, model);
            return true;
        }
    }
}
