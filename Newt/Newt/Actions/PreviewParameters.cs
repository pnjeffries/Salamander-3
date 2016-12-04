using FreeBuild.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Actions
{
    /// <summary>
    /// A set of parameters used when generating geometry to be displayed
    /// during a dynamic preview
    /// </summary>
    public class PreviewParameters
    { 
        #region Properties

        /// <summary>
        /// Is this a dynamic preview (as during point selection)?
        /// </summary>
        public bool IsDynamic { get; set; } = false;

        /// <summary>
        /// The input property currently being modified
        /// </summary>
        public PropertyInfo Input { get; set; } = null;

        /// <summary>
        /// The current cursor position
        /// </summary>
        public Vector CursorPoint { get; set; } = Vector.Unset;

        /// <summary>
        /// The current selection base point (if any)
        /// </summary>
        public Vector BasePoint { get; set; } = Vector.Unset;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PreviewParameters() { }

        /// <summary>
        /// Initialises a new set of Preview Parameters
        /// </summary>
        /// <param name="isDynamic"></param>
        /// <param name="input"></param>
        /// <param name="cursorPoint"></param>
        /// <param name="basePoint"></param>
        public PreviewParameters(bool isDynamic, PropertyInfo input, Vector cursorPoint, Vector basePoint)
        {
            IsDynamic = isDynamic;
            Input = input;
            CursorPoint = cursorPoint;
            BasePoint = basePoint;
        }

        #endregion
    }
}
