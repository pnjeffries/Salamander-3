using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Salamander.Grasshopper;

namespace Salamander.BasicToolsGH
{
    public class RobotExportComponent : SalamanderBaseComponent
    { 

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9d665fb9-a7c9-44dc-8e03-482cdf9e3c2e}"); }
        }

        public RobotExportComponent()
            : base("SaveAsRobot", "Export Robot", "Robot", SubCategories.Export)
        { }
    }
}