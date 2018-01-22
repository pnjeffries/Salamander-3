using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.UI
{
    /// <summary>
    /// An Avatar Factory to create WPF equivalent avatars (where possible)
    /// </summary>
    public class WPFAvatarFactory : IAvatarFactory
    {
        public ICoordinateSystemAvatar CreateCoordinateSystemAvatar()
        {
            throw new NotImplementedException();
        }

        public ICurveAvatar CreateCurveAvatar()
        {
            throw new NotImplementedException();
        }

        public ILabelAvatar CreateLabelAvatar()
        {
            throw new NotImplementedException();
        }

        public IMeshAvatar CreateMeshAvatar()
        {
            throw new NotImplementedException();
        }
    }
}
