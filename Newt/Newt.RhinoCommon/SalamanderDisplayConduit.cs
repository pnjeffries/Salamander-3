using Rhino.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Rhino
{
    public class SalamanderDisplayConduit : DisplayConduit
    {
        protected override void PostDrawObjects(DrawEventArgs e)
        {
            //Initialise rendering parameters:
            RhinoRenderingParameters parameters = new RhinoRenderingParameters(e);

            //Draw everything:
            //SalamanderHost.Instance.OrnamentTable.DrawAll(parameters);
            Core.Instance.Layers.Draw(parameters);
            //Draw current action preview
            /*if (Core.Instance.Actions.CurrentAction != null &&
                Core.Instance.Actions.CurrentAction.PreviewLayer != null)
            {
                Core.Instance.Actions.CurrentAction.PreviewLayer.Draw(parameters);
            }*/
        }
    }
}
