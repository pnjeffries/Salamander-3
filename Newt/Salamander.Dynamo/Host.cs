using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Base;
using Salamander.Display;

namespace Salamander.Dynamo
{
    /// <summary>
    /// Dynamo host class
    /// </summary>
    public class Host : IHost
    {
        public GUIController GUI => throw new NotImplementedException();

        public InputController Input => throw new NotImplementedException();

        public IAvatarFactory AvatarFactory => throw new NotImplementedException();

        public bool IsHidden(Unique unique)
        {
            return false;
        }

        public bool Print(string message)
        {
            return false;
        }

        public void Refresh()
        {
            
        }

        public bool Select(IList items, bool clear = false)
        {
            return false;
        }
    }
}
