using Nucleus.Extensions;
using Nucleus.Model;
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
    /// <summary>
    /// A value list set up to display standard catalogue section profile descriptions
    /// </summary>
    public class ChannelProfileCatalogueValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{B9CA53C5-9E05-470A-A606-CA5F8EB4D6EF}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                string uri1 = Resources.URIs.BlueBook;
                string uri2 = Resources.URIs.ChannelSection;
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2, 0, 2);
                return bmp;
            }
        }

        public ChannelProfileCatalogueValueList() : base()
        {
            Name = "Channel Profiles Catalogue";
            NickName = "Channel Profiles";
            Description = "A selector to choose standard channel section profile descriptions from the catalogue. Combine this with a 'Text to Section' component to generate a section family with this profile.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Sections;
            this.ListItems.Clear();

            SectionProfileCollection result = new SectionProfileCollection();
            Core.Instance.SectionLibrary.ExtractAllOfType(typeof(ChannelProfile), result);
            foreach (SectionProfile profile in result)
            {
                if (!string.IsNullOrWhiteSpace(profile.CatalogueName))
                ListItems.Add(new GH_ValueListItem(profile.CatalogueName, "\"" + profile.CatalogueName + "\""));
            }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {

        }

        public override void CreateAttributes()
        {
            base.m_attributes = new CatalogueValueListAttributes(this);
        }
    }
}
