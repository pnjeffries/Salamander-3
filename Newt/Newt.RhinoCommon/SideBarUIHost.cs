using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Salamander.Rhino
{
    [System.Runtime.InteropServices.Guid("CD5181CA-89A5-44D4-91B0-B50E779C13AA")]
    public partial class SideBarUIHost : UserControl
    {
        public SideBarUIHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Specifiy the element to be hosted
        /// </summary>
        /// <param name="element"></param>
        public void Host(UIElement element)
        {
            ElementHost.Child = element;
        }
    }
}
