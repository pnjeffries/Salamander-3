using Grasshopper.Kernel.Special;
using Salamander.Grasshopper;
using Salamander.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Salamander.BasicToolsGH
{
    public class DirectionValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{03095B9E-A6E9-43AF-AD6D-BF7D94E60F97}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                //string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri2 = IconResourceHelper.ResourceLocation + "Direction.png";
                Bitmap bmp = IconResourceHelper.BitmapFromURI(uri2);
                return bmp;
            }
        }

        public DirectionValueList() : base()
        {
            Name = "Direction";
            NickName = "Direction";
            Description = "A selector to choose between translational and rotational degrees of freedom in the X, Y, Z, XX, YY and ZZ directions.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Params;
            this.ListItems.Clear();
            ListItems.Add(new GH_ValueListItem("X", "\"X\""));
            ListItems.Add(new GH_ValueListItem("Y", "\"Y\""));
            ListItems.Add(new GH_ValueListItem("Z", "\"Z\""));
            ListItems.Add(new GH_ValueListItem("XX", "\"XX\""));
            ListItems.Add(new GH_ValueListItem("YY", "\"YY\""));
            ListItems.Add(new GH_ValueListItem("ZZ", "\"ZZ\""));
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
          
        }
    }
}
