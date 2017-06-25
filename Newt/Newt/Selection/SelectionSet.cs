using Nucleus.Model;
using Salamander.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    /// <summary>
    /// The set of currently selected objects
    /// </summary>
    public class SelectionSet
    {
        #region Properties

        /// <summary>
        /// The currently selected nodes
        /// </summary>
        public NodeSelection Nodes { get; } = new NodeSelection();

        /// <summary>
        /// The currently selected elements
        /// </summary>
        public ElementSelection Elements { get; } = new ElementSelection();

        /// <summary>
        /// The currently selected loads
        /// </summary>
        public LoadSelection Loads { get; } = new LoadSelection();

        /// <summary>
        /// The currently selected section properties
        /// </summary>
        public SectionFamilySelection SectionFamilies { get; } = new SectionFamilySelection();

        /// <summary>
        /// The currently selected panel families
        /// </summary>
        public BuildUpFamilySelection BuildUpFamilies { get; } = new BuildUpFamilySelection();

        /// <summary>
        /// The currently selected Materials
        /// </summary>
        public MaterialSelection Materials { get; } = new MaterialSelection();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectionSet()
        {
            SectionFamilies.MonitorElementSelectionSection(Elements);
            BuildUpFamilies.MonitorElementSelectionFamily(Elements);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add the specified object to the selection, if possible
        /// (i.e. if it is of a selectable type and does not already exist within the current selection)
        /// </summary>
        /// <param name="mObject">The object to select</param>
        /// <returns>True if the item was successfully added to the selection</returns>
        public bool Select(ModelObject mObject)
        {
            if (mObject == null) return false;
            else if (mObject is Element) return Elements.Add((Element)mObject);
            else if (mObject is Node) return Nodes.Add((Node)mObject);
            else if (mObject is Load) return Loads.Add((Load)mObject);
            else if (mObject is SectionFamily) return SectionFamilies.Add((SectionFamily)mObject);
            else if (mObject is BuildUpFamily) return BuildUpFamilies.Add((BuildUpFamily)mObject);
            else if (mObject is Material) return Materials.Add((Material)mObject);
            else return false;
        }

        /// <summary>
        /// Remove the specified object from 
        /// </summary>
        /// <param name="mObject"></param>
        /// <returns></returns>
        public bool Deselect(ModelObject mObject)
        {
            if (mObject == null) return false;
            else if (mObject is Element) return Elements.Remove((Element)mObject);
            else if (mObject is Node) return Nodes.Remove((Node)mObject);
            else if (mObject is Load) return Loads.Remove((Load)mObject);
            else if (mObject is SectionFamily) return SectionFamilies.Remove((SectionFamily)mObject);
            else if (mObject is BuildUpFamily) return BuildUpFamilies.Remove((BuildUpFamily)mObject);
            else if (mObject is Material) return Materials.Remove((Material)mObject);
            else return false;
        }

        /// <summary>
        /// Clear the selection
        /// </summary>
        public void Clear()
        {
            Elements.Clear();
            Nodes.Clear();
            Loads.Clear();
            SectionFamilies.Clear();
            BuildUpFamilies.Clear();
            Materials.Clear();
        }

        #endregion
    }
}
