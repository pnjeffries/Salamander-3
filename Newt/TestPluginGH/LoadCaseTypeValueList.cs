using Grasshopper.Kernel.Special;
using Nucleus.Model;
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
    public class LoadCaseTypeValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{BDA253FB-77FC-4689-B5CD-46EAF513715A}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                //string uri1 = IconResourceHelper.ResourceLocation + "ParamBackground.png";
                string uri1 = URIs.CaseType;
                string uri2 = URIs.DropDown;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2);
                return bmp;
            }
        }

        public LoadCaseTypeValueList() : base()
        {
            Name = "Load Case Types";
            NickName = "Load Case Type";
            Description = "A selector to choose between different load nature types.";
            Category = "Salamander 3";
            SubCategory = Grasshopper.SubCategories.Params;
            this.ListItems.Clear();
            foreach (var name in Enum.GetNames(typeof(LoadCaseType)))
            {
                ListItems.Add(new GH_ValueListItem(name, "\"" + name + "\""));
            }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

        }
    }
}
