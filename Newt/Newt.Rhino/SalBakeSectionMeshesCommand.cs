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
    public class SalBakeSectionMeshesCommand : Command
    {
        static SalBakeSectionMeshesCommand _instance;
        public SalBakeSectionMeshesCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalBakeSectionsCommand command.</summary>
        public static SalBakeSectionMeshesCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalBakeSectionMeshes"; }
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
            Host.Instance.Refresh();
            return Result.Success;
        }
    }
}
