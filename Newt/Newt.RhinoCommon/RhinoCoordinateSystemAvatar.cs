using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Rendering;
using Nucleus.Geometry;
using Nucleus.Rhino;

namespace Salamander.Rhino
{
    public class RhinoCoordinateSystemAvatar : RhinoAvatar, ICoordinateSystemAvatar
    {
        private ICoordinateSystem _CoordinateSystem;

        public ICoordinateSystem CoordinateSystem
        {
            set
            {
                _CoordinateSystem = value;
            }
        }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters && _CoordinateSystem != null)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                if (_CoordinateSystem is CartesianCoordinateSystem)
                {
                    CartesianCoordinateSystem cSystem = (CartesianCoordinateSystem)_CoordinateSystem;
                    rParams.Display.DrawDirectionArrow(FBtoRC.Convert(cSystem.Origin), FBtoRC.ConvertVector(cSystem.X), System.Drawing.Color.LightGray);
                    rParams.Display.DrawDirectionArrow(FBtoRC.Convert(cSystem.Origin), FBtoRC.ConvertVector(cSystem.Y), System.Drawing.Color.Gray);
                    rParams.Display.DrawDirectionArrow(FBtoRC.Convert(cSystem.Origin), FBtoRC.ConvertVector(cSystem.Z), System.Drawing.Color.Orange);
                }
                return true;
            }
            return false;
        }
    }
}
