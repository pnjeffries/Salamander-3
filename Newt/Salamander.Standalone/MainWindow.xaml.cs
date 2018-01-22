using Nucleus.Model;
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

namespace Salamander.Standalone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Host.EnsureInitialisation();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            // Open a document
            ModelDocument document = Core.Instance.OpenDocument(false);
            if (document != null) Core.Instance.ActiveDocument = document;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            // Save the current document
            Core.Instance.SaveDocument();
        }

        private void Command_Click(object sender, RoutedEventArgs e)
        {
            // Run the command specified by the tag property
            var fE = sender as FrameworkElement;
            string command = fE.Tag as string;
            Core.Instance.Execute(command);
        }
    }
}
