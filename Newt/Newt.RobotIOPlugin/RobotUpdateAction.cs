using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Salamander;
using Nucleus.Base;
using Nucleus.Model;
using Nucleus.Robot;
using Salamander;

namespace Newt.RobotIOPlugin
{
    [Action("UpdateRobot",
        Description = "Sync an existing Robot model to include updates made in Salamander via the COM interface.  Requires Robot to be installed.",
        IconBackground = Salamander.Resources.URIs.Robot,
        IconForeground = Salamander.Resources.URIs.UpdateIcon)]
    //[ExportAction(Extensions = new string[] { ".rtd" }, Filter = "Robot File (*.rtd) | *.rtd")]
    public class RobotExportAction : ModelActionBase//, IExportAction
    {
        [ActionInput(1, "write toggle.  Set to true to write out a Robot file", Manual = false)]
        public bool Write { get; set; } = true;

        [ActionFilePathInput(Order = 2,
            Description = "the filepath to write to",
            Filter = "Robot File (*.rtd) | *.rtd",
            Manual = true)]
        public FilePath FilePath { get; set; }

        [ActionInput(3, "update options", Required = false)]
        public RobotConversionOptions Options { get; set; } = new RobotConversionOptions(true);

        [ActionInput(4,
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
                if (Options == null) Options = new RobotConversionOptions(true);

                Document.Model.GenerateNodes(new NodeGenerationParameters());
                var robot = new RobotController();
                robot.Message += HandleMessage;
                RobotIDMappingTable idMap = null;
                if (Document.IDMappings.ContainsKey(FilePath)) idMap = Document.IDMappings[FilePath] as RobotIDMappingTable;
                if (idMap == null) idMap = Document.IDMappings.GetLatest(".rtd") as RobotIDMappingTable;
                if (idMap == null)
                {
                    idMap = new RobotIDMappingTable();
                    Document.IDMappings.Add(FilePath, idMap);
                }
                robot.UpdateRobotFromModel(FilePath, Model, ref idMap, Options);
                //robot.WriteModelToRobot(FilePath, Document.Model, ref idMap);
                robot.Close();
                robot.Release();
                Result = true;
            }
            return true;
        }
    }
}
