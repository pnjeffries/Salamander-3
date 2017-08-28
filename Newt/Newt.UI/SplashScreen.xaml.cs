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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        DispatcherTimer _Timer;
        bool _InitialisationComplete = false;
        int _CloseCount = 0;

        public SplashScreen(GUIController manager)
        {
            InitializeComponent();

            manager.UIInitialisationCompleted += HandlesInitialisationCompleted;
            _Timer = new DispatcherTimer();
            _Timer.Interval = new TimeSpan(TimeSpan.TicksPerSecond/100);
            _Timer.Tick += _Timer_Tick;
            _Timer.Start();
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            _CloseCount += 1;
            if (_CloseCount > 100 && _InitialisationComplete) Close();
        }

        public void HandlesInitialisationCompleted(object sender, EventArgs e)
        {
            _InitialisationComplete = true;
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
