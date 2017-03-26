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
    /// Interaction logic for ModelTree.xaml
    /// </summary>
    public partial class ModelTree : UserControl
    {
        public ModelTree()
        {
            InitializeComponent();
        }

        private void Elements_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Elements", Core.Instance.ActiveDocument.Model.Elements, Core.Instance.Selected.LinearElements);
        }

        private void CoordinateSystems_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Coordinate Systems", Core.Instance.ActiveDocument.Model.CoordinateSystems, null); //TODO!
        }

        private void Levels_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Levels", Core.Instance.ActiveDocument.Model.Levels, null); //TODO!
        }

        private void Materials_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Materials", Core.Instance.ActiveDocument.Model.Materials, null); //TODO!
        }

        private void Families_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Families", Core.Instance.ActiveDocument.Model.Families, Core.Instance.Selected.SectionProperties); //TODO: Fix!
        }

        private void Nodes_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Nodes", Core.Instance.ActiveDocument.Model.Nodes, Core.Instance.Selected.Nodes);
        }

        private void Sets_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Sets", Core.Instance.ActiveDocument.Model.Sets, null); //TODO!
        }

        private void Loads_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Loads", Core.Instance.ActiveDocument.Model.Loads, null); //TODO!
        }

        private void LoadCases_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.Instance.UI.ShowDataTable("Load Cases", Core.Instance.ActiveDocument.Model.LoadCases, null);
        }
    }
}
