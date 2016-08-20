using FreeBuild.Conversion;
using Newt.RhinoCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Grasshopper
{
    /// <summary>
    /// Manager class dealing with type conversion.
    /// Implemented as a singleton
    /// </summary>
    public class Conversion : ConversionLibrary
    {
        #region Properties

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Conversion Instance { get; } = new Conversion();

        #endregion

        #region Constructors

        /// <summary>
        /// Private Constructor
        /// </summary>
        private Conversion()
        {
            //Load default converters
            LoadConverters(typeof(FBtoRC));
            LoadConverters(typeof(RCtoFB));
        }

        #endregion
    }
}
