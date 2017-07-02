using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Salamander.Grasshopper;
using System.Drawing;
using Salamander.Resources;

namespace Salamander.BasicToolsGH
{
    public class SalamanderAutoBakeComponent : GH_Component
    {
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.tertiary;
            }
        }

        /// <summary>
        /// Initializes a new instance of the SalamanderAutoBakeComponent class.
        /// </summary>
        public SalamanderAutoBakeComponent()
          : base("Salamander Auto-Bake", "AutoBake",
              "Toggle for controlling the Salamander auto-bake global setting.  Determines whether Salamander components write to " +
                "the main Salamander model in Rhino or to a background model manipulatable only in Grasshopper.",
              "Salamander 3", SubCategories.Params)
        {
            
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Toggle", "T", "Toggle auto-baking.  If true, all Salamander components will write to the main model.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool toggle = false;
            if (!DA.GetData(0, ref toggle)) return;
            GrasshopperManager.Instance.AutoBake = toggle;
            GrasshopperManager.Instance.InvalidateAllSalamanderComponents(this.OnPingDocument());
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                string uri1 = IconResourceHelper.ResourceLocation + "BakeIcon.png";
                string uri2 = IconResourceHelper.ResourceLocation + "Salamander3_64x64.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri2, uri1);
                return bmp;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{007606c1-6b54-4fbb-b27d-09a998ed9e38}"); }
        }
    }
}