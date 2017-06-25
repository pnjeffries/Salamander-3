using Salamander.Display;
using Rhino.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Nucleus.Rendering;
using Nucleus.Rhino;

namespace Salamander.Rhino
{

    /// <summary>
    /// Base class for Rhino avatars
    /// </summary>
    public abstract class RhinoAvatar : Avatar
    {
        protected static DisplayMaterial _DefaultMaterial = CreateDefaultMaterial();
            

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

        public override DisplayBrush Brush
        {
            get
            {
                return null;
            }

            set
            {
                Material = FBtoRC.Convert(value);
            }
        }

        protected static DisplayMaterial CreateDefaultMaterial()
        {
            var result = new DisplayMaterial(Color.Gray, Color.White, Color.FromArgb(40, 40, 60), Color.FromArgb(60, 60, 60), 0.75, 0);
            return result;
        }

    }
}
