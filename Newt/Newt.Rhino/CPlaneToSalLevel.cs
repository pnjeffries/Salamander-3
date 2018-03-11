using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Salamander.Actions;
using Nucleus.Base;
using Nucleus.Model;
using Rhino.Geometry;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("E221165B-DB51-46A6-A540-C45F0F662BF8")]
    public class CPlaneToSalLevelCommand : Command
    {
        static CPlaneToSalLevelCommand _instance;
        public CPlaneToSalLevelCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalCommand2Command command.</summary>
        public static CPlaneToSalLevelCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "CPlaneToSalLevel"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();
            string levelName = "";
            try
            {
                levelName = Host.Instance.Input.EnterString(
                    Core.Instance.ActiveDocument.Model.Levels.GetNamesList(),
                    "Enter Salamander Level",
                    null);
                Level level = Core.Instance.ActiveDocument.Model.Levels.FindByName(levelName);
                if (level != null)
                {
                    Plane plane = new Plane(new Point3d(0, 0, level.Z), Vector3d.ZAxis);
                    doc.Views.ActiveView.ActiveViewport.SetConstructionPlane(plane);
                    doc.Views.ActiveView.Redraw();
                }
                else
                {
                    Core.Instance.Host.Print("No level named '" + levelName + "' found in the current Salamander document.");
                }
            }
            catch { }

            return Result.Success;
        }
    }
}