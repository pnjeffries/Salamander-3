using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Rendering;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Salamander.BasicTools
{
    public class NodeSupportDisplayLayer : DisplayLayer<Node>
    {
        public NodeSupportDisplayLayer() : base("Node Supports",
            "Display nodal restraint conditions", 2000,
            Resources.URIs.NodeSupport)
        {
            Visible = true;
        }

        public override IList<IAvatar> GenerateRepresentations(Node source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                NodeSupport support = source.GetData<NodeSupport>();
                if (support != null && !support.Fixity.AllFalse)
            {
                    double scale = 0.75;
                    IMeshAvatar mAv = CreateMeshAvatar();
                    mAv.Builder.AddNodeSupport(source, support, scale);
                    mAv.Brush = new ColourBrush(new Colour(0.8f, 0.8f, 0.2f, 0f)); //TODO: Make customisable
                    mAv.FinalizeMesh();
                    result.Add(mAv);
                }
            }
            return result;
        }

        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            base.InvalidateOnUpdate(modified, e);
            if (modified is Node) Core.Instance.Host.Refresh();
        }
    }
}
