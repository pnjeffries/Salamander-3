using Salamander.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RUI = Rhino.UI;
using R = Rhino;

namespace Salamander.Rhino
{
    /// <summary>
    /// GUIController implementation for user interface elements within Rhino.
    /// </summary>
    public class RhinoGUIController : WPFGUIController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RhinoGUIController()
        {
            RegisterPanelClasses();
        }

        /// <summary>
        /// Register Salamander's UI Panel with Rhino for later use
        /// </summary>
        protected void RegisterPanelClasses()
        {
            Guid pluginId = new Guid("31ef1c91-653d-406d-a253-f39547c8f45a");
            R.PlugIns.PlugIn plugin = R.PlugIns.PlugIn.Find(pluginId);
            if (plugin == null)
            {
                R.PlugIns.PlugIn.LoadPlugIn(pluginId);
                plugin = R.PlugIns.PlugIn.Find(pluginId);
            }
            R.UI.Panels.RegisterPanel(plugin, typeof(SideBarUIHost), "Salamander 3", Salamander.Rhino.Properties.Resources.SalamanderIcon);
               
        }

        /// <summary>
        /// Open the sidebar panel as a sibling of the layers table
        /// </summary>
        public void CreateHostDockPanel()
        {
            if (!RUI.Panels.IsPanelVisible(typeof(SideBarUIHost).GUID))
            {
                //RUI.Panels.OpenPanelAsSibling(typeof(SideBarUIHost).GUID, RUI.PanelIds.Layers);
                RUI.Panels.OpenPanel(typeof(SideBarUIHost).GUID);
            }

            SideBarControl selectPanel = new SideBarControl();
            selectPanel.DataContext = Core.Instance;
            SideBarUIHost host = (SideBarUIHost)RUI.Panels.GetPanel(typeof(SideBarUIHost).GUID);
            host.Host(selectPanel);

        }

        
    }
}
