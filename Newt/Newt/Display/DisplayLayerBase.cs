using FreeBuild.Base;
using FreeBuild.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Display
{
    /// <summary>
    /// Abstract base class for display layers
    /// </summary>
    public abstract class DisplayLayerBase : NotifyPropertyChangedBase, IRenderable
    {
        /// <summary>
        /// Internal backing member for Visible
        /// </summary>
        private bool _Visible = false;

        /// <summary>
        /// Layer visibility.
        /// </summary>
        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
                NotifyPropertyChanged("Visible");
            }
        }

        /// <summary>
        /// Is this display layer a dynamic preview layer that should be updated during position selection events
        /// </summary>
        public bool IsDynamic { get; protected set; }

        public abstract bool Draw(RenderingParameters parameters);

    }
}
