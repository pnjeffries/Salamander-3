using Nucleus.Base;
using Nucleus.Geometry;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Special;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Salamander.Resources;

namespace Salamander.BasicToolsGH
{
    public class Bool6DValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{325CAFB3-1174-41DC-9B46-3DB7EBC24C0F}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                //string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri1 = IconResourceHelper.ResourceLocation + "Bool6D.png";
                string uri2 = URIs.DropDown;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public Bool6DValueList() : base()
        {
            Name = "Bool6D";
            NickName = "Bool6D";
            Description = "A selector to toggle translational and rotational degrees of freedom in the X, Y, Z, XX, YY and ZZ directions.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Params;
            this.ListItems.Clear();
            ListItems.Add(new GH_ValueListItem("X", "X"));
            ListItems.Add(new GH_ValueListItem("Y", "Y"));
            ListItems.Add(new GH_ValueListItem("Z", "Z"));
            ListItems.Add(new GH_ValueListItem("XX", "XX"));
            ListItems.Add(new GH_ValueListItem("YY", "YY"));
            ListItems.Add(new GH_ValueListItem("ZZ", "ZZ"));
            ListMode = GH_ValueListMode.CheckList;
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

        }

        protected override void CollectVolatileData_Custom()
        {
            this.m_data.Clear();
            List<GH_ValueListItem>.Enumerator enumerator = this.SelectedItems.GetEnumerator();
            try
            {
                Bool6D result = new Bool6D();
                while (enumerator.MoveNext())
                {
                    GH_ValueListItem item = enumerator.Current;
                    Direction value = (Direction)Enum.Parse(typeof(Direction), item.Expression);
                    result = result.With(value, true);
                }
                this.m_data.Append(new Bool6DGoo(result), new GH_Path(0));
            }
            finally
            {
                ((System.IDisposable)enumerator).Dispose();
            }
        }
    }
}
