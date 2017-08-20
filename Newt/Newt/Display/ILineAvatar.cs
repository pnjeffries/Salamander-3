using Nucleus.Geometry;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    public interface ICurveAvatar : IAvatar
    {
        /// <summary>
        /// The curve geometry to be displayed
        /// </summary>
        Curve Curve { set; }

        /// <summary>
        /// Should the line be drawn dotted?
        /// </summary>
        bool Dotted { get; set; }
    }
}
