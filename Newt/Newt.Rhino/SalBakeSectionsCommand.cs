using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Nucleus.Model;
using Salamander.Display;
using Nucleus.Rhino;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("42bf55db-6771-4068-867d-fc04e7beff12")]
    public class SalBakeSectionsCommand : Command
    {
        static SalBakeSectionsCommand _instance;
        public SalBakeSectionsCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalBakeSectionsCommand command.</summary>
        public static SalBakeSectionsCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalBakeSections"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();

            LinearElementCollection elements = Core.Instance.ActiveDocument.Model.Elements.LinearElements;
            foreach (LinearElement lEl in elements)
            {
                RhinoMeshAvatar mAv = new RhinoMeshAvatar();
                ((IMeshAvatar)mAv).Builder.AddSectionPreview(lEl);
                ((IMeshAvatar)mAv).FinalizeMesh();
                RhinoOutput.BakeMesh(mAv.RenderMesh);
            }
            return Result.Success;
        }
    }
}
