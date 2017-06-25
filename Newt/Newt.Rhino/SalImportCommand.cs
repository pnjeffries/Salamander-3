using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Nucleus.Model;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("7431e926-b59d-4116-8b8a-286c6b28ecc4")]
    public class SalImportCommand : Command
    {
        static SalImportCommand _instance;
        public SalImportCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the NewtImportCommand command.</summary>
        public static SalImportCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalImport"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            ModelDocument document = Core.Instance.OpenDocument(false);
            if (document != null) Core.Instance.ActiveDocument = document;
            return Result.Success;
        }
    }
}
