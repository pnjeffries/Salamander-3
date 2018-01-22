using Nucleus.Model;
using Nucleus.Model.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// A selection of loads
    /// </summary>
    public class LoadSelection : SelectionViewModel<LoadCollection, Load>
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
            set { foreach (Load item in Selection) item.Name = value; }
        }

        /// <summary>
        /// Get or set the load case value of the objects in this selection
        /// </summary>
        public LoadCase Case
        {
            get { return CombinedValue(i => i.Case); }
            set
            {
                // TODO: Dummies
                foreach (var l in Selection) l.Case = value;
                NotifyPropertyChanged("Case");
            }
        }

        public LoadCaseCollection AvailableCases
        {
            get
            {
                if (Selection.Count > 0)
                {
                    LoadCaseCollection result = new LoadCaseCollection(Selection[0].Model?.LoadCases);
                    // result.Add(new SectionFamilyDummy("New...")); //TODO
                    return result;
                }
                else return null;
            }
        }

        public string Value
        {
            get { return CombinedValue(i => i.Value?.ToString(), "[Multi]", "0"); }
            set { foreach (var l in Selection) l.Value = value; }
        }

    }
}
