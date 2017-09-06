using Nucleus.Model;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Nucleus.Geometry;

namespace Salamander.BasicTools
{
    public class ElementLocalAxesDisplayLayer : DisplayLayer<Element>
    {
        public ElementLocalAxesDisplayLayer()
            : base("Element Local Axes",
                  "Display the local coordinate systems of elements",
                  4000,
                  Resources.URIs.CoordinateSystem)
        { }

        public override IList<IAvatar> GenerateRepresentations(Element source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null && source.GetGeometry() != null)
            {
                ICoordinateSystemAvatar cSAv = CreateCoordinateSystemAvatar();
                if (source is LinearElement)
                {
                    var lEl = (LinearElement)source;
                    cSAv.CoordinateSystem = lEl.Geometry.LocalCoordinateSystem(0.5, lEl.Orientation);
                }
                else if (source is PanelElement)
                {
                    var pEl = (PanelElement)source;
                    cSAv.CoordinateSystem = pEl.Geometry.LocalCoordinateSystem(pEl.Orientation);
                }
                result.Add(cSAv);
            }
            return result;
        }

        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            base.InvalidateOnUpdate(modified, e);
            if (e.PropertyName == "Orientation") Core.Instance.Host.Refresh();
        }
    }
}
