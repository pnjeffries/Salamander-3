using Nucleus.Extensions;
using Nucleus.Geometry;
using Nucleus.Model;
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
    public class ElementSelection : SelectionViewModel<ElementCollection, Element>
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
            set { foreach (Element item in Selection) item.Name = value; }
        }

        /// <summary>
        /// Does this selection contain only linear elements?
        /// </summary>
        public bool IsAllLinearElements
        {
            get { return Selection.ContainsOnlyType(typeof(LinearElement)); }
        }

        /// <summary>
        /// Does this selection contain only panel elements?
        /// </summary>
        public bool IsAllPanelElements
        {
            get { return Selection.ContainsOnlyType(typeof(PanelElement)); }
        }

        /// <summary>
        /// The selected element geometry
        /// </summary>
        public virtual VertexGeometry Geometry
        {
            get
            {
                if (Selection.Count == 1) return Selection[0].GetGeometry();
                else return null;
            }
        }

        /// <summary>
        /// Combined Orientation value
        /// </summary>
        public virtual Angle Orientation
        {
            get { return CombinedValue(i => i.Orientation, Angle.Multi, Angle.Undefined); }
            set { foreach (Element item in Selection) item.Orientation = value; }
        }

        /// <summary>
        /// Get or set the combined value of the section properties of the elements
        /// within this collection.
        /// </summary>
        public SectionFamily SectionFamily
        {
            get
            {
                if (IsAllLinearElements)
                    return (SectionFamily)CombinedValue(i => ((IElement)i).Family, null);
                else return null;
            }
            set
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
                foreach (LinearElement lEl in Selection) lEl.Family = value;
               
                NotifyPropertyChanged("SectionFamily");
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

        /// <summary>
        /// Get or set the combined value of the section properties of the elements
        /// within this collection.
        /// </summary>
        public BuildUpFamily PanelFamily
        {
            get
            {
                if (IsAllPanelElements)
                    return (BuildUpFamily)CombinedValue(i => ((IElement)i).Family, null);
                else return null;
            }
            set
            {
                if (value != null && value is PanelFamilyDummy)
                {
                    PanelFamilyDummy dummy = (PanelFamilyDummy)value;
                    if (dummy.Name.Equals("New..."))
                    {
                        BuildUpFamily newSection = Selection[0].Model?.Create.BuildUpFamily();
                        value = newSection;
                        NotifyPropertyChanged("AvailablePanelFamilies");
                    }
                }
                foreach (PanelElement lEl in Selection) lEl.Family = value;

                NotifyPropertyChanged("PanelFamily");
            }
        }

        /// <summary>
        /// The set of panel families which are available to be assigned to the elements
        /// in this collection.
        /// </summary>
        public BuildUpFamilyCollection AvailablePanelFamilies
        {
            get
            {
                if (Selection.Count > 0)
                {
                    BuildUpFamilyCollection result = new BuildUpFamilyCollection(Selection[0].Model?.Families.PanelFamilies);
                    result.Add(new PanelFamilyDummy("New..."));
                    return result;
                }
                else return null;
            }
        }

        public IList<MultiElementVertex> ElementVertices
        {
            get
            {
                if (Selection.Count == 1)
                {
                    return Selection[0].ElementVertices.ToMultiElementVertices();
                }
                else if (Selection.Count > 1)
                {
                    return Selection.GetMergedElementVertices();
                }
                else return null;
            }
        }
    }
}
