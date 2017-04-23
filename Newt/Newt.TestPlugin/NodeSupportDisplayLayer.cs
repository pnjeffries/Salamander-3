﻿using FreeBuild.Geometry;
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
    public class NodeSupportDisplayLayer : DisplayLayer<Node>
    {
        public NodeSupportDisplayLayer() : base("Node Supports",
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
                    mAv.Builder.AddNodeSupport(source, support);
                    mAv.Brush = new ColourBrush(new Colour(0.5f, 0.8f, 0.2f, 0f)); //TODO: Make customisable
                    mAv.FinalizeMesh();
                    result.Add(mAv);
                }
            }
            return result;
        }
    }
}