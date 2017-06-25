using Nucleus.Rendering;
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
        #region Properties

        public abstract DisplayBrush Brush { get; set; }

        /// <summary>
        /// This avatar's GUID.
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Should this avatar be drawn?
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Can this avatar's geometry be 'baked'?
        /// </summary>
        public virtual bool CanBake
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw this avatar
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract bool Draw(RenderingParameters parameters);

        /// <summary>
        /// 'Bake' this avatar's geometry into the document -
        /// i.e. make it into a fixed, editable form in the current application.
        /// </summary>
        /// <returns>True if the 'bake' was successful</returns>
        public virtual bool Bake()
        {
            return false;
        }

        #endregion

    }
}
