using FreeBuild.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    /// <summary>
    /// Base class for avatars - objects drawn in the 3D view to represent data
    /// </summary>
    public abstract class Avatar : IRenderable, IAvatar
    {
        /// <summary>
        /// This avatar's GUID.
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Should this avatar be drawn?
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Draw this avatar
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract bool Draw(RenderingParameters parameters);
    }
}
