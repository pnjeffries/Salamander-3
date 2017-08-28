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
    public class ElementReleasesDisplayLayer : DisplayLayer<Element>
    {

        public ElementReleasesDisplayLayer()
            : base("Element Releases", "Show Element end and vertex releases", 5000, Resources.URIs.ElementReleases)
        { }

        public override IList<IAvatar> GenerateRepresentations(Element source)
        {
            var result = new List<IAvatar>();
            double scale = 0.5;
            double f = 0.707 * scale;
            double off = 0.1;
            var verts = source.ElementVertices;
            foreach (var vert in verts)
            {
                if (!vert.Releases.AllFalse)
                {
                    CartesianCoordinateSystem cSystem = vert.LocalCoordinateSystem;
                    if (cSystem != null)
                    {
                        if (vert.Releases.X)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Line(cSystem.LocalToGlobal(scale, 0, 0), cSystem.LocalToGlobal(-scale, 0, 0)))));
                        }
                        if (vert.Releases.Y)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Line(cSystem.LocalToGlobal(off, scale, 0), cSystem.LocalToGlobal(off, -scale, 0)))));
                        }
                        if (vert.Releases.Z)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Line(cSystem.LocalToGlobal(off, 0, scale), cSystem.LocalToGlobal(off, 0, -scale)))));
                        }
                        if (vert.Releases.XX)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Arc(cSystem.LocalToGlobal(off, f, f), cSystem.LocalToGlobal(off, 0, scale), cSystem.LocalToGlobal(off, f, -f)))));
                        }
                        if (vert.Releases.YY)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Arc(cSystem.LocalToGlobal(f, 0, f), cSystem.LocalToGlobal(scale, 0, 0), cSystem.LocalToGlobal(f, 0, -f)))));
                        }
                        if (vert.Releases.ZZ)
                        {
                            result.Add(
                                FormatAvatar(
                                    CreateCurveAvatar(
                                        new Arc(cSystem.LocalToGlobal(f, f, 0), cSystem.LocalToGlobal(scale, 0, 0), cSystem.LocalToGlobal(f, -f, 0)))));
                        }
                    }
                }
            }
            return result;
        }

        private ICurveAvatar FormatAvatar(ICurveAvatar cAv)
        {
            cAv.Brush = new ColourBrush(new Colour(0.5f, 0.8f, 0.2f, 0f)); //TODO: Make customisable
            cAv.ArrowStart = true;
            cAv.ArrowEnd = true;
            return cAv;
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
            //if (modified is Node)
            //{
            //    Node sp = (Node)modified;
            //    ElementCollection els = sp.GetConnectedElements();
            //    foreach (Element lEl in els)
            //    {
            //        InvalidateRepresentation(lEl);
            //    }
            //    Core.Instance.Host.Refresh();
            //}
            //else
            //{
            if (modified is Element && e.PropertyName.EndsWith("Releases"))
            {
                InvalidateRepresentation((Element)modified);
                Core.Instance.Host.Refresh();
            }
            else
                base.InvalidateOnUpdate(modified, e);
                //if (modified is LinearElement && (e.PropertyName == "Family" || e.PropertyName == "Orientation")) Core.Instance.Host.Refresh();
            //}
        }
    }
    //TODO!
}
