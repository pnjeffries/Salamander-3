using Salamander.Selection;
using System;
using System.Collections;
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
    /// Interaction logic for DataTable.xaml
    /// </summary>
    public partial class DataTable : UserControl
    {
        /// <summary>
        /// Static callback function to raise a ValueChanged event
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((DataTable)d).GenerateDisplayPropertiesColumns();
        }

        /// <summary>
        /// Items Source dependency property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(DataTable), 
                new PropertyMetadata(new PropertyChangedCallback(OnItemsSourceChanged)));

        /// <summary>
        /// The source collection for the items in this data table
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Selected Items dependency property
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(DataTable),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The currently selected items in this data table
        /// </summary>
        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>
        /// Selection mode dependency property
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(DataGridSelectionMode), typeof(DataTable),
                new PropertyMetadata(DataGridSelectionMode.Extended));

        /// <summary>
        /// The selection mode of the table.
        /// Wraps the selection mode of the inner datagrid
        /// </summary>
        public DataGridSelectionMode SelectionMode
        {
            get { return (DataGridSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        /// <summary>
        /// The selection view model to be synchronised with this table's selectedItems
        /// </summary>
        public SelectionViewModel LinkedSelection { get; set; } = null;

        public DataTable()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
            //this.DataContextChanged += DataTable_DataContextChanged;
        }

        //private void DataTable_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    GenerateDisplayPropertiesColumns();
        //}

        public DataTable(IEnumerable itemsSource) : this()
        {
            ItemsSource = itemsSource;
        }

        public DataTable(IEnumerable itemsSource, SelectionViewModel selection) : this(itemsSource)
        {
            if (selection != null)
            {
                LinkedSelection = selection;

                //Doesn't work!
                /*Binding selectionBinding = new Binding();
                selectionBinding.Source = selection;
                selectionBinding.Path = new PropertyPath("Selection");
                selectionBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
//selectionBinding.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(this, DataTable.SelectedItemsProperty, selectionBinding);*/
            }
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
