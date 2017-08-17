using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Nucleus.Model;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("7431e926-b59d-4116-8b8a-286c6b28ecc4")]
    public class SalOpenCommand : Command
    {
        static SalOpenCommand _instance;
        public SalOpenCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the NewtImportCommand command.</summary>
        public static SalOpenCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalOpen"; }
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
