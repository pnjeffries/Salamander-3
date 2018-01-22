using Nucleus.Actions;
using Nucleus.Base;
using Nucleus.GSA;
using Nucleus.Model;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.GSA
{
    [Action("OpenGWA", "Read a model from a GSA text (GWA) format file")]
    [ImportAction(Extensions = new string[] { ".gwa" }, Filter = "GSA Text File (*.gwa) | *.gwa")]
    public class OpenGWA : ModelActionBase, IImportDocumentAction
    {
        [ActionFilePathInput(Order = 0,
            Description = "the GWA file to import",
            Open = true,
            Filter = "GSA Text File (*.gwa) | *.gwa")]
        public FilePath FilePath { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var parser = new GWAParser();
            GSAIDMappingTable idMap = new GSAIDMappingTable();
            Model model = parser.ReadGWAFile(FilePath, ref idMap);
            Document = new ModelDocument(FilePath, model);
            Document.IDMappings[FilePath] = idMap;

            return true;
        }
    }
}
