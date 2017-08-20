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
    class RhinoCurveAvatar : RhinoAvatar, ICurveAvatar
    {
        /// <summary>
        /// The line to draw
        /// </summary>
        public RC.Curve RenderCurve { get; set; }

        /// <summary>
        /// Whether the line should be drawn dotted
        /// </summary>
        public bool Dotted { get; set; }

        Curve ICurveAvatar.Curve
        {
            set
            {
                RenderCurve = FBtoRC.Convert(value);
            }
        }

        public RhinoCurveAvatar() { }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters && RenderCurve != null)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                if (RenderCurve is RC.LineCurve)
                {
                    RC.Line line = ((RC.LineCurve)RenderCurve).Line;
                    if (Dotted) rParams.Display.DrawDottedLine(line, Material.Diffuse);
                    else rParams.Display.DrawLine(line, Material.Diffuse);
                }
                else
                {
                    rParams.Display.DrawCurve(RenderCurve, Material.Diffuse);
                }
                return true;
            }
            return false;
        }
    }
}
