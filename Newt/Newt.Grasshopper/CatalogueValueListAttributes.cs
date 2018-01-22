using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Special;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Extensions;

namespace Salamander.Grasshopper
{
    public class CatalogueValueListAttributes : GH_ValueListAttributes
    {
        public CatalogueValueListAttributes(GH_ValueList owner) : base(owner) { }

        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RectangleF rectangleF;
                GH_ValueListItem item2 = this.Owner.FirstSelectedItem;
                if (item2 != null)
                {
                    rectangleF = item2.BoxRight;
                    if (rectangleF.Contains(e.CanvasLocation))
                    {
                        ToolStripDropDownMenu menu = new ToolStripDropDownMenu();
                        GH_ValueListItem activeItem = this.Owner.FirstSelectedItem;

                        var dict = new Dictionary<string, IList<GH_ValueListItem>>();
                        foreach (GH_ValueListItem listItem in this.Owner.ListItems)
                        {
                            string key = listItem.Name.StartingLetters();
                            if (!dict.ContainsKey(key)) dict[key] = new List<GH_ValueListItem>();
                            dict[key].Add(listItem);
                        }

                        if (dict.Keys.Count > 1)
                        {
                            foreach (var kvp in dict)
                            {
                                var menuItems = new List<ToolStripMenuItem>(kvp.Value.Count);
                                foreach (GH_ValueListItem listItem in kvp.Value)
                                {
                                    ToolStripMenuItem menuItem = new ToolStripMenuItem(listItem.Name);
                                    menuItem.Click += this.ValueMenuItem_Click;
                                    if (listItem == activeItem)
                                    {
                                        menuItem.Checked = true;
                                    }
                                    menuItem.Tag = listItem;
                                    menuItems.Add(menuItem);
                                }
                                ToolStripMenuItem keyItem = new ToolStripMenuItem(kvp.Key + "...", null, menuItems.ToArray());
                                menu.Items.Add(keyItem);
                            }
                        }
                        else
                        {
                            //Fallback: one category only
                            foreach (GH_ValueListItem listItem in this.Owner.ListItems)
                            {
                                ToolStripMenuItem menuItem = new ToolStripMenuItem(listItem.Name);
                                menuItem.Click += this.ValueMenuItem_Click;
                                if (listItem == activeItem)
                                {
                                    menuItem.Checked = true;
                                }
                                menuItem.Tag = listItem;
                                menu.Items.Add(menuItem);
                            }
                        }
                        menu.Show(sender, e.ControlLocation);
                        return GH_ObjectResponse.Handled;
                    }
                }
       
            }
            return base.RespondToMouseDown(sender, e);
        }

        private void ValueMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            if (!menuItem.Checked)
            {
                GH_ValueListItem item = menuItem.Tag as GH_ValueListItem;
                if (item != null)
                {
                    this.Owner.SelectItem(this.Owner.ListItems.IndexOf(item));
                }
            }
        }
    }
}
