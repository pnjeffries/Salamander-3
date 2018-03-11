using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;

namespace Salamander.BasicTools
{
    [Action("CreateLevel",
        "Create a new level",
        IconBackground = Resources.URIs.Level,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateLevel : ModelActionBase
    {
        [ActionInput(1, "the name of the new level", Manual = false)]
        public string Name { get; set; } = null;

        [ActionInput(2, "the z-level")]
        public double Z { get; set; } = 0.0;

        [ActionOutput(3, "the created level")]
        public Level Level { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Level = Model.Create.Level(Z, exInfo);
            Level.Name = Name;
            return true;
        }
    }
}
