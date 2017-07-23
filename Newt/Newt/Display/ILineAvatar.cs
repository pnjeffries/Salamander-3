using Nucleus.Geometry;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    public interface ILineAvatar : IAvatar
    {
        /// <summary>
        /// The line geometry to be displayed
        /// </summary>
        Line Line { set; }

        /// <summary>
        /// Should the line be drawn dotted?
        /// </summary>
        bool Dotted { get; set; }
    }
}
