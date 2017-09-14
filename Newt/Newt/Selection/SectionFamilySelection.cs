using Nucleus.Base;
using Nucleus.Extensions;
using Nucleus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    public class SectionFamilySelection : SelectionViewModel<SectionFamilyCollection, SectionFamily>
    {
        /// <summary>
        /// The primary selected section for individual property display - will be the last selected section
        /// </summary>
        public SectionFamily Section
        {
            get
            {
                if (Selection.Count > 0)
                {
                    SectionFamily result = Selection.Last();
                    return result;
                }
                else return null;
            }
            set
            {
                if (value != null)
                {
                    if (value != null && value is SectionFamilyDummy)
                    {
                        SectionFamilyDummy dummy = (SectionFamilyDummy)value;
                        if (dummy.Name.Equals("New..."))
                        {
                            SectionFamily newSection = Selection[0].Model?.Create.SectionFamily();
                            value = newSection;
                            NotifyPropertyChanged("AvailableSections");
                        }
                    }

                    if (Selection.Contains(value.GUID)) Selection.Remove(value);
                    Selection.Add(value);
                }
            }
        }

        /// <summary>
        /// The set of sections which are available to be assigned to the elements
        /// in this collection.
        /// </summary>
        public SectionFamilyCollection AvailableSections
        {
            get
            {
                if (Selection.Count > 0)
                {
                    SectionFamilyCollection result = new SectionFamilyCollection(Selection[0].Model?.Families.Sections);
                    result.Add(new SectionFamilyDummy("New..."));
                    return result;
                }
                else return null;
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

        public SectionFamilySelection()
        {

        }

        public SectionFamilySelection(SectionFamily section)
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

        public void MonitorElementSelectionSection(ElementSelection elementSelection)
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
                e.PropertyName == "SectionFamily"))
            {
                ElementSelection elementSelection = (ElementSelection)sender;
                object selected = elementSelection.SectionFamily;
                if (selected != null && selected is SectionFamily)
                {
                    Set((SectionFamily)selected);
                }
                else Clear();
            }
        }
    }
}
