using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBuild.Rendering;
using FreeBuild.Rhino;
using Salamander.Display;
using R = Rhino;
using RC = Rhino.Geometry;
using FreeBuild.Meshing;
using FreeBuild.Model;
using System.Drawing;

namespace Salamander.Rhino
{
    public class RhinoMeshAvatar : RhinoAvatar, IMeshAvatar
    {

        /// <summary>
        /// The mesh to be used for rendering
        /// </summary>
        public RC.Mesh RenderMesh { get; set; } = null;

        /// <summary>
        /// Internal backing member for Build property
        /// </summary>
        private RhinoMeshBuilder _Build;

        MeshBuilderBase IMeshAvatar.Builder
        {
            get
            {
                if (_Build == null) _Build = new RhinoMeshBuilder();
                return _Build;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RhinoMeshAvatar()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renderMesh"></param>
        public RhinoMeshAvatar(RC.Mesh renderMesh)
        {
            RenderMesh = renderMesh;
        }

        /// <summary>
        /// Constructor for a StructAnalysis1DElement section preview
        /// </summary>
        /// <param name="element"></param>
        public RhinoMeshAvatar(LinearElement element)
        {
            RhinoMeshBuilder rmb = new RhinoMeshBuilder();
            rmb.AddSectionPreview(element);
            RenderMesh = rmb.Mesh;
            RenderMesh.Normals.ComputeNormals();
        }

        public override bool Draw(RenderingParameters parameters)
        {
            if (parameters is RhinoRenderingParameters && RenderMesh != null)
            {
                RhinoRenderingParameters rParams = (RhinoRenderingParameters)parameters;
                if (rParams.Display.DisplayPipelineAttributes.ShadingEnabled)
                {
                    rParams.Draw(RenderMesh, Material);
                }
                else
                {
                    rParams.DrawWireframe(RenderMesh, Material.Diffuse);
                }
                return true;
            }
            else return false;
        }

        bool IMeshAvatar.FinalizeMesh()
        {
            if (_Build != null)
            {
                _Build.Mesh.Normals.ComputeNormals();
                RenderMesh = _Build.Mesh;
                return true;
            }
            return false;
        }
    }
}
