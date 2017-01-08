using FreeBuild.Model;
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
        /// The currently selected linear elements
        /// </summary>
        public ElementSelection LinearElements { get; } = new ElementSelection();

        /// <summary>
        /// The currently selected section properties
        /// </summary>
        public SectionPropertySelection SectionProperties { get; } = new SectionPropertySelection();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectionSet()
        {
            SectionProperties.MonitorElementSelectionSection(LinearElements);
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
            else if (mObject is LinearElement) return LinearElements.Add((LinearElement)mObject);
            else if (mObject is Node) return Nodes.Add((Node)mObject);
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
            else if (mObject is LinearElement) return LinearElements.Remove(mObject.GUID);
            else if (mObject is Node) return Nodes.Remove(mObject.GUID);
            else return false;
        }

        /// <summary>
        /// Clear the selection
        /// </summary>
        public void Clear()
        {
            LinearElements.Clear();
            Nodes.Clear();
        }

        #endregion
    }
}
