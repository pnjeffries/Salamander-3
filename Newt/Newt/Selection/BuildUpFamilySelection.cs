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
    public class BuildUpFamilySelection : SelectionViewModel<BuildUpFamilyCollection, BuildUpFamily>
    {
        /// <summary>
        /// The primary selected family for individual property display - will be the last selected family
        /// </summary>
        public BuildUpFamily Family
        {
            get
            {
                if (Selection.Count > 0)
                {
                    BuildUpFamily result = Selection.Last();
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
                return Family?.Layers[0].Thickness ?? 0;
            }
            set
            {
                EnsureOneLayer();
                Family.Layers[0].Thickness = value;
            }
        }

        public Material Material
        {
            get
            {
                EnsureOneLayer();
                return Family?.Layers[0].Material;
            }
            set
            {
                EnsureOneLayer();
                if (Family != null) Family.Layers[0].Material = value;
            }
        }

        #region Methods

        private void EnsureOneLayer()
        {
            if (Family != null && Family.Layers.Count == 0)
                Family.Layers.Add(new BuildUpLayer());
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
                if (selected != null && selected is BuildUpFamily)
                {
                    Set((BuildUpFamily)selected);
                }
                else Clear();
            }
        }

        #endregion
    }
}
