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
    public class CHSProfileCatalogueValueList : GH_ValueList
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{16BD7E0B-91C2-4901-A753-192ED52C7898}");
            }
        }

        protected override Bitmap Icon
        {
            get
            {
                string uri1 = IconResourceHelper.ResourceLocation + "BlueBook.png";
                string uri2 = IconResourceHelper.ResourceLocation + "CircularHollowSection.png";
                Bitmap bmp = IconResourceHelper.CombinedBitmapFromURIs(uri1, uri2, 0, 2);
                return bmp;
            }
        }

        public CHSProfileCatalogueValueList() : base()
        {
            Name = "CHS-Profiles Catalogue";
            NickName = "CHS-Profiles";
            Description = "A selector to choose standard circular hollow section profile descriptions from the catalogue. Combine this with a 'Text to Section' component to generate a section family with this profile.";
            Category = "Salamander 3";
            SubCategory = SubCategories.Sections;
            this.ListItems.Clear();

            SectionProfileCollection result = new SectionProfileCollection();
            Core.Instance.SectionLibrary.ExtractAllOfType(typeof(CircularHollowProfile), result);
            foreach (SectionProfile profile in result)
            {
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
