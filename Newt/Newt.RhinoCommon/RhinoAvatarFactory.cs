using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Rhino
{
    public class RhinoAvatarFactory : IAvatarFactory
    {
        public ILineAvatar CreateLineAvatar()
        {
            return new RhinoLineAvatar();
        }

        public IMeshAvatar CreateMeshAvatar()
        {
            return new RhinoMeshAvatar();
        }
    }
}
