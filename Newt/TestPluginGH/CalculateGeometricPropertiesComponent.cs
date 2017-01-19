using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Salamander.Grasshopper;

namespace Salamander.TestPluginGH
{
    public class CalculateGeometricPropertiesComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{F36B9018-952A-426B-A2C0-2CA18315F90C}");
            }
        }

        public CalculateGeometricPropertiesComponent()
            :base("GeometricProperties",
                 "Geometric Properties",
                 "Geo",
                 "Test")
        {

        }
    }
}
