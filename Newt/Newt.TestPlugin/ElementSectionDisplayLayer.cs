using FreeBuild.Model;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Salamander.BasicTools
{
    /// <summary>
    /// A DisplayLayer which draws 3D section profile representations over 1D Elements
    /// </summary>
    public class ElementSectionDisplayLayer : DisplayLayer<LinearElement>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ElementSectionDisplayLayer() : base("Element Sections",
            "Display Elements with 3D Section Profile representations",
            1000,
            Resources.URIs.SectionPreview)
        {
            Visible = true;
        }

        /// <summary>
        /// Override function which generates and returns a set of representation 'avatar' objects for the given source object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override IList<IAvatar> GenerateRepresentations(LinearElement source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                IMeshAvatar mAv = CreateMeshAvatar();
                mAv.Builder.AddSectionPreview(source);
                mAv.FinalizeMesh();
                result.Add(mAv);
            }
            return result;
        }

        /// <summary>
        /// Override function which invalidates the representations of objects as necessary when a design update occurs
        /// In this case, the section representations need to be updated whenever the section geometry changes as well as
        /// the element geometry (which is done by default)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="e"></param>
        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            if (modified is SectionFamily)
            {
                SectionFamily sp = (SectionFamily)modified;
                ElementCollection els = sp.Elements();
                foreach (LinearElement lEl in els)
                {
                    InvalidateRepresentation(lEl);
                }
                Core.Instance.Host.Refresh();
            }
            else
            {
                base.InvalidateOnUpdate(modified, e);
                if (modified is LinearElement && (e.PropertyName == "Family" || e.PropertyName == "Orientation")) Core.Instance.Host.Refresh();
            }
        }
    }
}
