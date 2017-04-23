using Grasshopper.Kernel.Special;
using System;
using System.Collections.Generic;
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

        public DirectionValueList() : base()
        {
            Name = "Direction";
            NickName = "Direction";
            Description = "A selector to choose between translational and rotational degrees of freedom in the X, Y, Z, XX, YY and ZZ directions.";
            Category = "Salamander 3";
            SubCategory = "Params";
            this.ListItems.Clear();
            ListItems.Add(new GH_ValueListItem("X", "1"));
            ListItems.Add(new GH_ValueListItem("Y", "2"));
            ListItems.Add(new GH_ValueListItem("Z", "3"));
            ListItems.Add(new GH_ValueListItem("XX", "4"));
            ListItems.Add(new GH_ValueListItem("YY", "5"));
            ListItems.Add(new GH_ValueListItem("ZZ", "6"));
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
          
        }
    }
}
