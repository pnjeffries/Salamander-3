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

        /// <summary>
        /// Should an arrow head be drawn at the start of the curve?
        /// </summary>
        public bool ArrowStart { get; set; } = false;

        /// <summary>
        /// Should an arrow head be drawn at the end of the curve?
        /// </summary>
        public bool ArrowEnd { get; set; } = false;

        Curve ICurveAvatar.Curve
        {
            set
            {
                RenderCurve = ToRC.Convert(value);
            }
        }

        public RhinoCurveAvatar() { }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters && RenderCurve != null)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                if (RenderCurve is RC.LineCurve && Dotted) //?
                {
                    RC.Line line = ((RC.LineCurve)RenderCurve).Line;
                    if (Dotted) rParams.Display.DrawDottedLine(line, Material.Diffuse);
                    else rParams.Display.DrawLine(line, Material.Diffuse);
                }
                else
                {
                    rParams.Display.DrawCurve(RenderCurve, Material.Diffuse, 3);
                }
                double arrS = ToRC.Convert(0.3);
                if (ArrowStart) rParams.Display.DrawArrowHead(RenderCurve.PointAtStart - arrS*RenderCurve.TangentAtStart, -RenderCurve.TangentAtStart, Material.Diffuse, 0, arrS);
                if (ArrowEnd) rParams.Display.DrawArrowHead(RenderCurve.PointAtEnd + arrS * RenderCurve.TangentAtEnd, RenderCurve.TangentAtEnd, Material.Diffuse, 0, arrS);
                return true;
            }
            return false;
        }
    }
}
