using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("7b344dad-3e7f-4a07-b158-4a10d09aba77")]
    public class SalExportCommand : Command
    {
        static SalExportCommand _instance;
        public SalExportCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalExportCommand command.</summary>
        public static SalExportCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalExport"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            Core.Instance.SaveModel();
            return Result.Success;
        }
    }
}
