using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Nucleus.Model;
using Nucleus.Rhino;
using Rhino.Geometry;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("7020f1b9-3677-4c42-af65-4d8592970e82")]
    public class SalBakeSectionExtrusionsCommand : Command
    {
        static SalBakeSectionExtrusionsCommand _instance;
        public SalBakeSectionExtrusionsCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalBakeSectionExtrusions command.</summary>
        public static SalBakeSectionExtrusionsCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalBakeSectionExtrusions"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();

            LinearElementCollection elements = Core.Instance.ActiveDocument.Model.Elements.LinearElements;
            foreach (LinearElement lEl in elements)
            {
                Extrusion extrusion = FBtoRC.ConvertToExtrusion(lEl);
                if (extrusion != null)
                    RhinoOutput.BakeExtrusion(extrusion);
                else
                {
                    Brep brep = FBtoRC.ConvertToBrep(lEl);
                    //if (brep != null) RhinoOutput.Bake
                }
            }
            return Result.Success;
        }
    }
}
