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
                if (!lEl.IsDeleted)
                {
                    Extrusion extrusion = NtoRC.ConvertToExtrusion(lEl);
                    Guid guid = Guid.Empty;
                    if (extrusion != null)
                        guid = RhinoOutput.BakeExtrusion(extrusion);
                    else
                    {
                        Brep brep = NtoRC.ConvertToBrep(lEl);
                        if (brep != null) guid = RhinoOutput.Bake(brep);
                    }
                    if (guid != null)
                    {
                        RhinoOutput.SetObjectName(guid, lEl.Name);
                        if (lEl.Family != null)
                        {
                            RhinoOutput.SetObjectUserString(guid, "Family", lEl.Family.Name);
                            if (lEl.Family.GetPrimaryMaterial() != null) RhinoOutput.SetObjectUserString(guid, "Material", lEl.Family.GetPrimaryMaterial().Name);
                            if (lEl.Family.Profile != null) RhinoOutput.SetObjectUserString(guid, "Profile", lEl.Family.Profile.ToString());
                        }
                    }
                }
            }
            Host.Instance.Refresh();

            return Result.Success;
        }
    }
}
