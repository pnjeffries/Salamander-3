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
using System.Windows.Threading;

namespace Salamander.UI
{
    /// <summary>
    /// Interaction logic for SideBarControl.xaml
    /// </summary>
    public partial class SideBarControl : UserControl
    {
        public SideBarControl()
        {
            InitializeComponent();

            LayoutBase.DataContext = Core.Instance;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => Core.Instance.UI.NotifyInitialisationCompleted()), DispatcherPriority.ContextIdle, null);
        }
    }
}
