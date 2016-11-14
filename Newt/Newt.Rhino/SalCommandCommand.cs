﻿using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("c59b62e4-9b41-4539-b002-559c75d4914a")]
    public class SalCommandCommand : Command
    {
        static SalCommandCommand _instance;
        public SalCommandCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the NewtCommandCommand command.</summary>
        public static SalCommandCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "S3Command"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            string command = Host.Instance.Input.EnterString("Enter Salamander command");
            Core.Instance.Execute(command);
            return Result.Success;
        }
    }
}