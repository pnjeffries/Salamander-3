using System;
using Rhino;
using Rhino.Commands;
using Newt.RhinoCommon;

namespace Newt.RhinoPlugin
{
    [System.Runtime.InteropServices.Guid("c59b62e4-9b41-4539-b002-559c75d4914a")]
    public class NewtCommandCommand : Command
    {
        static NewtCommandCommand _instance;
        public NewtCommandCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the NewtCommandCommand command.</summary>
        public static NewtCommandCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "NewtCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            string command = Host.Instance.Input.EnterString("Enter Newt command");
            Core.Instance.Execute(command);
            return Result.Success;
        }
    }
}
