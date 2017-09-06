using Nucleus.Model;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    public class NodeNumberDisplayLayer : DisplayLayer<Node>
    {
        public NodeNumberDisplayLayer()
            : base("Node IDs", "Display Node Numeric IDs", 6000, Resources.URIs.NodeID) { }

        public override IList<IAvatar> GenerateRepresentations(Node source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                var labelAv = CreateLabelAvatar();
                labelAv.Label = new Nucleus.Geometry.Label(source.Position, source.NumericID.ToString());
                result.Add(labelAv);
            }
            return result;
        }
    }
}
