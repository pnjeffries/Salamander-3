using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Rendering;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    public class ElementOffsetDisplayLayer : DisplayLayer<Element>
    {
        private DisplayBrush _Brush = ColourBrush.White;

        public ElementOffsetDisplayLayer() : base("Element Offsets", "Display the links between nodes and element vertices", 3000, Resources.URIs.ElementOffsets) { Visible = true; }

        public override IList<IAvatar> GenerateRepresentations(Element source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null && source.GetGeometry() != null)
            {
                foreach (var vertex in source.GetGeometry().Vertices)
                {
                    if (vertex.Node != null && !vertex.NodalOffset().IsZero())
                    {
                        ICurveAvatar lAv = CreateCurveAvatar();
                        lAv.Curve = new Line(vertex.Node.Position, vertex.Position);
                        lAv.Dotted = true;
                        lAv.Brush = _Brush;
                        result.Add(lAv);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Override function which invalidates the representations of objects as necessary when a design update occurs
        /// In this case, the offset display needs to be updated whenever node geometry changes as well as
        /// the element geometry (which is done by default)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="e"></param>
        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            if (modified is Node)
            {
                Node sp = (Node)modified;
                ElementCollection els = sp.GetConnectedElements();
                foreach (Element lEl in els)
                {
                    InvalidateRepresentation(lEl);
                }
                Core.Instance.Host.Refresh();
            }
            else
            {
                base.InvalidateOnUpdate(modified, e);
                //if (modified is LinearElement && (e.PropertyName == "Family" || e.PropertyName == "Orientation")) Core.Instance.Host.Refresh();
            }
        }
    }
}
