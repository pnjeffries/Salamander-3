using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS = Autodesk.DesignScript.Geometry;
using Nucleus.Geometry;

namespace Salamander.Dynamo
{
    /// <summary>
    /// Converter class to convert Nucleus geometry types to Dynamo equivalents
    /// </summary>
    public static class NtoD
    {
        /// <summary>
        /// Convert a Nucleus Vector to a DesignScript Point
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static DS.Point Convert(Vector pt)
        {
            return DS.Point.ByCoordinates(pt.X, pt.Y, pt.Z);
        }

        /// <summary>
        /// Convert a Nucleus Line to a DesignScript Line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static DS.Line Convert(Line line)
        {
            return DS.Line.ByStartPointEndPoint(Convert(line.StartPoint), Convert(line.EndPoint));
        }
    }
}
