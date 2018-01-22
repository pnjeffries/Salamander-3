using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Meshing;
using Nucleus.Rendering;
using Salamander.Display;
using Nucleus.WPF;

namespace Salamander.UI
{
    public class WPFMeshAvatar : WPFAvatar, IMeshAvatar
    {
        private DisplayBrush _Brush = null;

        public override DisplayBrush Brush
        {
            get { return _Brush; }
            set { _Brush = value; }
        }

        private WPFMeshBuilder _Builder;

        public MeshBuilderBase Builder
        {
            get
            {
                if (_Builder == null) _Builder = new WPFMeshBuilder();
                return _Builder;
            }
        }

        public override bool Draw(RenderingParameters parameters)
        {
            throw new NotImplementedException();
        }

        public bool FinalizeMesh()
        {
            throw new NotImplementedException();
        }
    }
}
