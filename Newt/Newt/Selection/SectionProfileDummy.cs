using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Geometry;

namespace Salamander.Selection
{
    public class SectionProfileDummy : SectionProfile
    {
        public override Curve Perimeter => null;

        public override CurveCollection Voids => null;

        public override double OverallDepth => 0;

        public override double OverallWidth => 0;

        public override string GenerateDescription()
        {
            return CatalogueName; ;
        }

        public override Vector GetTotalOffset(HorizontalSetOut toHorizontal = HorizontalSetOut.Centroid, VerticalSetOut toVertical = VerticalSetOut.Centroid)
        {
            throw new NotImplementedException();
        }

        public SectionProfileDummy()
        {
            CatalogueName = "";
        }

    }
}
