using Nucleus.Actions;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("CreateMaterial", "Create a new material",
        IconBackground = Resources.URIs.Material,
        IconForeground = Resources.URIs.AddIcon)]
    public class CreateMaterial : ModelActionBase
    {
        [ActionInput(1, "the name of the new material")]
        public string Name { get; set; }

        [ActionOutput(1, "the created material")]
        public Material Material {get;set;}

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Material = Model.Create.IsoMaterial(Name, exInfo);
            return true;
        }
    }
}
