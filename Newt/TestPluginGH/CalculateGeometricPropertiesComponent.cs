using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Salamander.Grasshopper;

namespace Salamander.TestPluginGH
{
    public class CalculateGeometricPropertiesComponent : NewtBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{B8F850F3-8867-452F-8F30-7E921C3C3FF8}");
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
