using Nucleus.Model;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    public class ElementNumberDisplayLayer : DisplayLayer<Element>
    {
        public ElementNumberDisplayLayer()
            : base("Node IDs", "Display Node Numeric IDs", 6100, Resources.URIs.ElementID) { }

        public override IList<IAvatar> GenerateRepresentations(Element source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                var labelAv = CreateLabelAvatar();
                labelAv.Label = new Nucleus.Geometry.Label(source.GetNominalPosition(), source.NumericID.ToString());
                result.Add(labelAv);
            }
            return result;
        }
    }
}