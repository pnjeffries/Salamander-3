using FreeBuild.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Rhino
{
    /// <summary>
    /// Table linking unique objects to handles
    /// </summary>
    public class HandlesLink : BiDirectionary<Guid,Guid>
    {
        /// <summary>
        /// Get the ID of the handle that is linked to the specified source object ID
        /// </summary>
        /// <param name="forSource">The ID of a source object</param>
        /// <returns></returns>
        public Guid GetHandleID(Guid forSource)
        {
            return _AtoB[forSource];
        }

        /// <summary>
        /// Get the ID of the handle that is linked to the specified source object ID
        /// </summary>
        /// <param name="forSource">The ID of a source object</param>
        /// <returns></returns>
        public Guid GetHandleID(Unique forSource)
        {
            return GetHandleID(forSource.GUID);
        }

        /// <summary>
        /// Get the ID of the handle that is 
        /// </summary>
        /// <param name="forHandle"></param>
        /// <returns></returns>
        public Guid GetSourceID(Guid forHandle)
        {
            return _BtoA[forHandle];
        }
    }
}
