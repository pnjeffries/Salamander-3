﻿using Newt.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Rhino
{
    public class RhinoAvatarFactory : IAvatarFactory
    {
        public IMeshAvatar CreateMeshAvatar()
        {
            return new RhinoMeshAvatar();
        }
    }
}