using System;
using Rhino;
using Rhino.Commands;
using Salamander.RhinoCommon;
using Nucleus.Model;
using Salamander.Display;
using Nucleus.Rhino;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("d017261d-81ca-4474-ae8f-1c343c40a865")]
    public class SalBakePanelsCommand : Command
    {
        static SalBakePanelsCommand _instance;
        public SalBakePanelsCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SalBakeBuildUpsCommand command.</summary>
        public static SalBakePanelsCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "SalBakePanels"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Host.EnsureInitialisation();

            PanelElementCollection elements = Core.Instance.ActiveDocument.Model.Elements.PanelElements;
            foreach (PanelElement lEl in elements)
            {
                RhinoMeshAvatar mAv = new RhinoMeshAvatar();
                ((IMeshAvatar)mAv).Builder.AddPanelPreview(lEl);
                ((IMeshAvatar)mAv).FinalizeMesh();
                RhinoOutput.BakeMesh(mAv.RenderMesh);
            }
            return Result.Success;
        }
    }
}
