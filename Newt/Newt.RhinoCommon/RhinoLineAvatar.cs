using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Rendering;
using Nucleus.Geometry;
using RC = Rhino.Geometry;
using Nucleus.Rhino;
using System.Drawing;

namespace Salamander.Rhino
{
    /// <summary>
    /// A Rhino line avatar
    /// </summary>
    class RhinoLineAvatar : RhinoAvatar, ILineAvatar
    {
        /// <summary>
        /// The line to draw
        /// </summary>
        public RC.Line RenderLine { get; set; }

        /// <summary>
        /// Whether the line should be drawn dotted
        /// </summary>
        public bool Dotted { get; set; }

        Line ILineAvatar.Line
        {
            set
            {
                RenderLine = FBtoRC.ConvertToLine(value);
            }
        }

        public RhinoLineAvatar() { }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                if (Dotted) rParams.Display.DrawDottedLine(RenderLine, Material.Diffuse);
                else rParams.Display.DrawLine(RenderLine, Material.Diffuse);
                return true;
            }
            return false;

        }
    }
}
