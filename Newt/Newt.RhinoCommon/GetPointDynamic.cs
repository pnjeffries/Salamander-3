using FreeBuild.Rhino;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Salamander.Actions;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Rhino
{
    /// <summary>
    /// An extended GetPoint with custom DynamicDraw functionality
    /// </summary>
    public class GetPointDynamic : GetPoint
    {
        protected override void OnDynamicDraw(GetPointDrawEventArgs e)
        {
            IAction action = Core.Instance.Actions.CurrentAction;
            if (action != null)
            {
                Point3d basePt;
                TryGetBasePoint(out basePt);
                var pParam = new PreviewParameters(true, null, RCtoFB.Convert(e.CurrentPoint), RCtoFB.Convert(basePt));
                DisplayLayer previewLayer = action.PreviewLayer(pParam);
                if (previewLayer != null)
                {
                    var rParam = new RhinoRenderingParameters(e);
                    previewLayer.Draw(rParam);
                }
            }

            base.OnDynamicDraw(e);
        }
    }
}
