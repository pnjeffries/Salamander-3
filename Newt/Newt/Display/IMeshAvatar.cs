using FreeBuild.Meshing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Display
{
    /// <summary>
    /// An avatar type that represents data as a 3D mesh of vertices and faces.
    /// Provides a MeshBuilder instance that can be used to add mesh geometry in a platform-agnostic manner.
    /// </summary>
    public interface IMeshAvatar : IAvatar
    {
        /// <summary>
        /// A MeshBuilder instance that can be used to add mesh geometry in a platform-agnostic manner.
        /// Use this property to specify the mesh geometry to be displayed, 
        /// then call FinalizeMesh to complete and assign the mesh once finished.
        /// </summary>
        MeshBuilderBase Builder { get; }

        /// <summary>
        /// Complete final mesh building operations and assign the resulting mesh to this avatar.
        /// Call this once you have finished defining the mesh geometry using the MeshBuilder instance accessed
        /// via the Builder property.
        /// </summary>
        /// <returns></returns>
        bool FinalizeMesh();
    }
}
