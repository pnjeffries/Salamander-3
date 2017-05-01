using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// A selection of Panel Family objects
    /// </summary>
    public class PanelFamilySelection : SelectionViewModel<PanelFamilyCollection, PanelFamily>
    {
        /// <summary>
        /// The primary selected family for individual property display - will be the last selected family
        /// </summary>
        public PanelFamily Family
        {
            get
            {
                if (Selection.Count > 0)
                {
                    PanelFamily result = Selection.Last();
                    return result;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    if (Selection.Contains(value.GUID)) Selection.Remove(value);
                    Selection.Add(value);
                }
            }
        }

        public double Thickness
        {
            get
            {
                EnsureOneLayer();
                return Family?.BuildUp[0].Thickness ?? 0;
            }
            set
            {
                EnsureOneLayer();
                Family.BuildUp[0].Thickness = value;
            }
        }

        public Material Material
        {
            get
            {
                EnsureOneLayer();
                return Family?.BuildUp[0].Material;
            }
            set
            {
                EnsureOneLayer();
                if (Family != null) Family.BuildUp[0].Material = value;
            }
        }

        #region Methods

        private void EnsureOneLayer()
        {
            if (Family != null && Family.BuildUp.Count == 0)
                Family.BuildUp.Add(new BuildUpLayer());
        }

        public void MonitorElementSelectionFamily(ElementSelection elementSelection)
        {
            elementSelection.PropertyChanged += HandlesElementPropertyChanged;
        }

        /// <summary>
        /// Handle a change of section profile in the element selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlesElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ElementSelection && (e.PropertyName == null ||
                e.PropertyName == "PanelFamily"))
            {
                ElementSelection elementSelection = (ElementSelection)sender;
                object selected = elementSelection.PanelFamily;
                if (selected != null && selected is PanelFamily)
                {
                    Set((PanelFamily)selected);
                }
                else Clear();
            }
        }

        #endregion
    }
}
