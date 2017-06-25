using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Salamander.RhinoCommon;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("68f6b39d-75cc-465e-addd-f3bf2a40d969")]
    public class SalamanderCommand : Command
    {
        public SalamanderCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static SalamanderCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "Salamander3"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            Host.Instance.GUI.CreateHostDockPanel();
            return Result.Success;
        }
    }
}
