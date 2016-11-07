using System;
using Rhino;
using Rhino.Commands;
using Newt.RhinoCommon;

namespace Newt.Rhino
{
    [System.Runtime.InteropServices.Guid("7431e926-b59d-4116-8b8a-286c6b28ecc4")]
    public class NewtImportCommand : Command
    {
        static NewtImportCommand _instance;
        public NewtImportCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the NewtImportCommand command.</summary>
        public static NewtImportCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "NewtImport"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            Core.Instance.OpenDocument(false);
            return Result.Success;
        }
    }
}
