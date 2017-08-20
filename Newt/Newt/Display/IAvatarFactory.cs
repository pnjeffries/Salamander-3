using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    /// <summary>
    /// An interface which describes object capable of producing avatars of different types
    /// </summary>
    public interface IAvatarFactory
    {
        /// <summary>
        /// Create and return a new MeshAvatar
        /// </summary>
        /// <returns></returns>
        IMeshAvatar CreateMeshAvatar();

        /// <summary>
        /// Create and return a new CurveAvatar
        /// </summary>
        /// <returns></returns>
        ICurveAvatar CreateCurveAvatar();

        /// <summary>
        /// Create and return new CoordinateSystemAvatar
        /// </summary>
        /// <returns></returns>
        ICoordinateSystemAvatar CreateCoordinateSystemAvatar();

        /// <summary>
        /// Create and return a new PointAvatar
        /// </summary>
        /// <returns></returns>
        //IPointAvatar CreatePointAvatar();
    }
}
