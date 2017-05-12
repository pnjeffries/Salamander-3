using FreeBuild.Extensions;
using FreeBuild.Model;
using Grasshopper.Kernel.Special;
using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Salamander.BasicToolsGH
{
    /// <summary>
    /// A value list set up to display standard catalogue section profile descriptions
    /// </summary>
    public class IProfileCatalogueValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{3038B5A1-535C-4FA5-A7EC-A834BBEEF0F6}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "BlueBook.png";
                string uri2 = IconResourceHelper.ResourceLocation + "SectionFamily.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2, 0, 2);
                return bmp;
            }
        }

        public IProfileCatalogueValueList() : base()
        {
            Name = "I-Profiles Catalogue";
            NickName = "I-Profiles";
            Description = "A selector to choose standard I-shaped section profile descriptions from the catalogue. Combine this with a 'Text to Section' component to generate a section family with this profile.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Sections;
            this.ListItems.Clear();
            
            SectionProfileCollection result = new SectionProfileCollection();
            Core.Instance.SectionLibrary.ExtractAllOfType(typeof(SymmetricIProfile), result);
            foreach (SectionProfile profile in result)
            {
                ListItems.Add(new GH_ValueListItem(profile.CatalogueName, "\"" + profile.CatalogueName + "\""));
            }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

        }
    }
}
