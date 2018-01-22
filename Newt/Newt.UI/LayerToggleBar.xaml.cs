using Nucleus.UI;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Salamander.UI
{
    /// <summary>
    /// Interaction logic for LayerToggleBar.xaml
    /// </summary>
    public partial class LayerToggleBar : UserControl
    {
        public LayerToggleBar()
        {
            InitializeComponent();
        }

        private void RefreshViewport(object sender, RoutedEventArgs e)
        {
            Core.Instance.Host.Refresh();
        }

        private void ToggleButton_RMUp(object sender, MouseButtonEventArgs e)
        {
            var fE = sender as FrameworkElement;
            var layer = fE?.DataContext as DisplayLayer;
            if (layer != null && layer is IAutoUIHostable)
                Core.Instance.Host.GUI.ShowFieldsDialog("Display Options", layer);
        }
    }
}
