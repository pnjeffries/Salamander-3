using FreeBuild.Geometry;
using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// A selection of Linear Elements
    /// </summary>
    public class ElementSelection : SelectionViewModel<LinearElementCollection, LinearElement>
    {
        /// <summary>
        /// Get or set the combined name value of the objects in this collection.
        /// If all the objects in this collection have the same name, that name will be returned.
        /// Otherwise the string "[Multi]" will be returned.
        /// Set this property to set the name property of all objects in this collection
        /// </summary>
        public virtual string Name
        {
            get { return CombinedValue(i => i.Name, "[Multi]"); }
            set { foreach (LinearElement item in Selection) item.Name = value; }
        }

        public virtual Curve Geometry
        {
            get
            {
                if (Selection.Count == 1) return Selection[0].Geometry;
                else return null;
            }
        }

        /// <summary>
        /// Combined Orientation value
        /// </summary>
        public virtual Angle Orientation
        {
            get { return CombinedValue(i => i.Orientation, Angle.Multi, Angle.Undefined); }
            set { foreach (LinearElement item in Selection) item.Orientation = value; }
        }

        /// <summary>
        /// Set or set the combined value of the section properties of the elsments
        /// within this collection.
        /// </summary>
        public SectionFamily Property
        {
            get { return CombinedValue(i => i.Family, null); }
            set
            {
                if (value != null && value is SectionPropertyDummy)
                {
                    SectionPropertyDummy dummy = (SectionPropertyDummy)value;
                    if (dummy.Name.Equals("New..."))
                    {
                        SectionFamily newSection = Selection[0].Model?.Create.SectionProperty();
                        value = newSection;
                        NotifyPropertyChanged("AvailableSections");
                    }
                }
                foreach (LinearElement lEl in Selection) lEl.Family = value;
               
                NotifyPropertyChanged("Family");
                NotifyPropertyChanged("Section");
            }
        }

        private SectionPropertySelection _Section = new SectionPropertySelection();

        public SectionPropertySelection Section
        {
            get
            {
                _Section.Section = Property;
                return _Section;
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
                    result.Add(new SectionPropertyDummy("New..."));
                    return result;
                }
                else return null;
            }
        }
    }
}
