using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Geometry;
using Nucleus.Rendering;
using Salamander.Display;
using Nucleus.Rhino;

namespace Salamander.Rhino
{
    public class RhinoLabelAvatar : RhinoAvatar, ILabelAvatar
    {
        private Label _Label = null;

        public Label Label
        {
            set
            {
                _Label = value;
            }
        }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters && _Label != null)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                var scrPt = rParams.Display.Viewport.WorldToClient(ToRC.Convert(_Label.Position));
                scrPt.X += 2;
                scrPt.Y -= 2;
                rParams.Display.Draw2dText(_Label.Text, System.Drawing.Color.GhostWhite, scrPt, !_Label.HorizontalSetOut.IsEdge());
                return true;
            }
            return false;
        }
    }
}
