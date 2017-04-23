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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen(GUIController manager)
        {
            InitializeComponent();

            manager.UIInitialisationCompleted += HandlesInitialisationCompleted;
        }

        public void HandlesInitialisationCompleted(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
