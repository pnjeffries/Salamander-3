using FreeBuild.Geometry;
using FreeBuild.Model;
using FreeBuild.Rendering;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    public class NodeRestraintDisplayLayer : DisplayLayer<Node>
    {
        public NodeRestraintDisplayLayer() : base("Node Restraints",
            "Display nodal restraint conditions", 2000,
            Resources.BaseURI + "NodeRestraint.png")
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
                    double scale = 0.5;
                    IMeshAvatar mAv = CreateMeshAvatar();
                    var tip = source.Position;
                    Vector direction = new Vector(0, 0, scale);
                    var circle = new Circle(scale, new CylindricalCoordinateSystem(tip - direction, new Vector(new Angle(Math.PI / 4)), Vector.UnitY));
                    mAv.Builder.AddFacetCone(tip, circle, 4);
                    mAv.Brush = new ColourBrush(new Colour(0.5f, 0.8f, 0.2f, 0f));
                    mAv.FinalizeMesh();
                    result.Add(mAv);
                }
            }
            return result;
        }
    }
}
