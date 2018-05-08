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
    public class AxesReferenceValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{D7B1DFC7-CD31-41C1-B379-13D341DF68C7}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                string uri1 = URIs.CoordinateSystem;
                string uri2 = URIs.DropDown;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1,uri2);
                return bmp;
            }
        }

        public AxesReferenceValueList() : base()
        {
            Name = "Coordinate System References";
            NickName = "CSRef";
            Description = "A selector to choose between standard coordinate system references.  Useful when specifying load application axes.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Params;
            ListItems.Clear();
            ListItems.Add(new GH_ValueListItem("Global", "\"Global\""));
            ListItems.Add(new GH_ValueListItem("Local", "\"Local\""));
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

        }
    }
}
