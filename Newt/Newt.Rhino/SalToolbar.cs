using System;
using Rhino;
using Rhino.Commands;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("59943b58-a343-4837-8b89-6eb94fac3cf3")]
    [CommandStyle(Style.ScriptRunner)]
    public class SalToolbar : Command
    {
        static SalToolbar _instance;
        public SalToolbar()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalToolbar command.</summary>
        public static SalToolbar Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalToolbar"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            RhinoApp.RunScript("ShowToolbar \"Salamander 3\"", true);
            return Result.Success;
        }
    }
}
