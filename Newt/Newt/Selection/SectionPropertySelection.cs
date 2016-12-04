using FreeBuild.Base;
using FreeBuild.Extensions;
using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    public class SectionPropertySelection : SelectionViewModel<SectionPropertyCollection, SectionProperty>
    {
        /// <summary>
        /// The primary selected section for individual property display - will be the last selected section
        /// </summary>
        public SectionProperty Section
        {
            get
            {
                if (Selection.Count > 0)
                {
                    SectionProperty result = Selection.Last();
                    //if (result.StructuralModifiers == null) result.StructuralModifiers = new ProfileStructuralModifiers();
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

        public SectionProfile CatalogueName
        {
            get
            {
                if (Section == null || Section.Profile == null || Section.Profile.CatalogueName == null) return null;
                else return Core.Instance.SectionLibrary.GetByCatalogueName(Section.Profile.CatalogueName);
            }
            set
            {
                if (Section != null && Section.Profile != null)
                {
                    if (value == null) Section.Profile.CatalogueName = null;
                    else
                    {
                        Section.Profile.CopyPropertiesFrom(value);
                    }
                }
            }
        }

        public SectionProfileCollection AvailableCatalogue
        {
            get
            {
                if (Section != null && Section.Profile != null)
                {
                    SectionProfileCollection result = new SectionProfileCollection();
                    Core.Instance.SectionLibrary.ExtractAllOfType(Section.ProfileType, result);
                    return result;
                }
                return null;
            }
        }

        public SectionPropertySelection()
        {

        }

        public SectionPropertySelection(SectionProperty section)
        {
            Section = section;
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "ProfileType")
            {
                NotifyPropertyChanged("AvailableCatalogue");
            }
            else if (propertyName == "Profile")
            {
                NotifyPropertyChanged("CatalogueName");
            }
        }

        /*public void MonitorElementSelectionSection(StructAnalysis1DElementSelection elementSelection)
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
            if (sender is StructAnalysis1DElementSelection && (e.PropertyName == null ||
                e.PropertyName == PropertyNames.STRUCTANALYSIS1DELEMENT_SECTIONPROFILE))
            {
                StructAnalysis1DElementSelection elementSelection = (StructAnalysis1DElementSelection)sender;
                object selected = elementSelection.SectionProfile;
                if (selected != null && selected is SectionProfile)
                {
                    Set((SectionProfile)selected);
                }
                else Clear();
            }
        }*/
    }
}
