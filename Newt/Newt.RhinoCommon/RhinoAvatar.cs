using Newt.Display;
using Rhino.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Rhino
{

    /// <summary>
    /// Base class for Rhino avatars
    /// </summary>
    public abstract class RhinoAvatar : Avatar
    {
        protected static DisplayMaterial _DefaultMaterial = new DisplayMaterial();

        private DisplayMaterial _Material = null;

        /// <summary>
        /// The material to be used for rendering
        /// </summary>
        public DisplayMaterial Material
        {
            get
            {
                if (_Material != null) return _Material;
                else return GetDefaultMaterial();
            }
            set
            {
                _Material = value;
            }
        }

        /// <summary>
        /// Get the default material for this type of mesh
        /// </summary>
        /// <returns></returns>
        public DisplayMaterial GetDefaultMaterial()
        {
            return _DefaultMaterial;
        }

    }
}
