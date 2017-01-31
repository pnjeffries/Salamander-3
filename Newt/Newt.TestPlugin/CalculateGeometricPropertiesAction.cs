using FreeBuild.Actions;
using FreeBuild.Geometry;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [Action("GeometricProperties",
        "Calculate the geometric properties of a profile")]
    public class CalculateGeometricPropertiesAction : ActionBase
    {
        [ActionInput(1,"the profile of which calculate the geometric properties")]
        public Curve Profile { get; set; }

        [ActionOutput(1,"the area of the profile")]
        public double Area { get; set; }

        [ActionOutput(2,"the centroid of the profile")]
        public Vector Centroid { get; set; }

        [ActionOutput(3,"the second moment of area of the profile about the X-axis", ShortName = "Ixx")]
        public double Ixx { get; set; }

        [ActionOutput(4,"the second moment of area of the profile about the Y-axis", ShortName = "Iyy")]
        public double Iyy { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Vector centroid;
            Area = Profile.CalculateEnclosedArea(out centroid);
            Centroid = centroid;
            Ixx = Profile.CalculateEnclosedIxx(new Plane(centroid));
            Iyy = Profile.CalculateEnclosedIxx(new Plane(centroid, Vector.UnitY, -Vector.UnitX));
            return true;
        }
    }
}
