using Nucleus.Model;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    class PanelThicknessDisplayLayer : Display.DisplayLayer<PanelElement>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelThicknessDisplayLayer() : base("Panel Thickness",
            "Display Panel Elements with 3D thickness representations",
            1000,
            Resources.URIs.PanelPreview)
        {
            Visible = true;
        }

        /// <summary>
        /// Override function which generates and returns a set of representation 'avatar' objects for the given source object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override IList<IAvatar> GenerateRepresentations(PanelElement source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                IMeshAvatar mAv = CreateMeshAvatar();
                mAv.Builder.AddPanelPreview(source);
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
            if (modified is BuildUpFamily)
            {
                BuildUpFamily sp = (BuildUpFamily)modified;
                ElementCollection els = sp.Elements();
                foreach (PanelElement lEl in els)
                {
                    InvalidateRepresentation(lEl);
                }
                Core.Instance.Host.Refresh();
            }
            else
            {
                base.InvalidateOnUpdate(modified, e);
                if (modified is PanelElement && (e.PropertyName == "Family")) Core.Instance.Host.Refresh();
            }
        }
    
    }
}
