using Nucleus.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    /// <summary>
    /// Interface for label display avatars
    /// </summary>
    public interface ILabelAvatar : IAvatar
    {
        /// <summary>
        /// The label to be displayed
        /// </summary>
        Label Label { set; }
    }
}
