using Nucleus.Geometry;
using Nucleus.Rhino;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Salamander.Actions;
using Salamander.Display;
using Rhino.Display;
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
        /// <summary>
        /// The currently selected positions.
        /// If null the base point and current point will be used instead.
        /// </summary>
        public IList<Vector> SelectionPoints { get; set; } = null;

        protected override void OnDynamicDraw(GetPointDrawEventArgs e)
        {
            IAction action = Core.Instance.Actions.CurrentAction;
            if (action != null)
            {
                PreviewParameters pParam;
                if (SelectionPoints == null)
                {
                    Point3d basePt;
                    TryGetBasePoint(out basePt);
                    pParam = new PreviewParameters(true, null, RCtoN.Convert(e.CurrentPoint), RCtoN.Convert(basePt));
                }
                else
                {
                    IList<Vector> sPts = new List<Vector>();
                    foreach (Vector pt in SelectionPoints)
                    {
                        sPts.Add(pt);
                        // Draw points:
                        e.Display.DrawPoint(NtoRC.Convert(pt), PointStyle.X, 3, System.Drawing.Color.Orange);
                    }
                    sPts.Add(RCtoN.Convert(e.CurrentPoint));
                    pParam = new PreviewParameters(true, null, sPts);

                    
                }
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
