using Nucleus.Rhino;
using Rhino.Display;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC = Rhino.Geometry;

namespace Salamander.Rhino
{
    public class SalamanderDisplayConduit : DisplayConduit
    {
        protected override void CalculateBoundingBox(CalculateBoundingBoxEventArgs e)
        {
            base.CalculateBoundingBox(e);
            RC.BoundingBox bBox = ToRC.Convert(Core.Instance.ActiveDocument.Model.BoundingBox);
            e.IncludeBoundingBox(bBox);
        }

        protected override void PostDrawObjects(DrawEventArgs e)
        {
            // Initialise rendering parameters:
            RhinoRenderingParameters parameters = new RhinoRenderingParameters(e);

            // Draw everything:
            // SalamanderHost.Instance.OrnamentTable.DrawAll(parameters);
            Core.Instance.Layers.Draw(parameters);

            // Draw current action preview:
            if (Core.Instance.Actions.CurrentAction != null)
            {
                DisplayLayer previewLayer = Core.Instance.Actions.CurrentAction.PreviewLayer(new Actions.PreviewParameters());
                if (previewLayer != null) previewLayer.Draw(parameters);
            }
        }
    }
}
