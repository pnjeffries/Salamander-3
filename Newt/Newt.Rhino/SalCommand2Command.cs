using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Salamander.Actions;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("B24D0C9B-1B54-4300-A837-8C7CD6911BA2")]
    public class SalCommand2Command : Command
    {
        static SalCommand2Command _instance;
        public SalCommand2Command()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalCommand2Command command.</summary>
        public static SalCommand2Command Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalCommand2"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            string command = "";
            try
            {
                command = Host.Instance.Input.EnterString(
                    Core.Instance.Actions.GetCommandList(),
                    "Enter Salamander command", 
                    Core.Instance.Actions.LastCommand?.GetCommandName());
                Core.Instance.Execute(command);
            }
            catch { }
            
            return Result.Success;
        }
    }
}

