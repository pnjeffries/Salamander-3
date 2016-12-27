using FreeBuild.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    public class NodeSelection : SelectionViewModel<NodeCollection, Node>
    {
        /// <summary>
        /// Get or set the combined name value of the objects in this collection.
        /// If all the objects in this collection have the same name, that name will be returned.
        /// Otherwise the string "[Multi]" will be returned.
        /// Set this property to set the name property of all objects in this collection
        /// </summary>
        public virtual string Name
        {
            get { return (string)CombinedValue(i => i.Name, "[Multi]"); }
            set { foreach (Node item in Selection) item.Name = value; }
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public double X
        {
            get { return CombinedValue(i => i.Position.X, double.NaN, double.NaN); }
            set { foreach (Node item in Selection) item.Position = item.Position.WithX(value); }
        }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y
        {
            get { return CombinedValue(i => i.Position.Y, double.NaN, double.NaN); }
            set { foreach (Node item in Selection) item.Position = item.Position.WithY(value); }
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public double Z
        {
            get { return CombinedValue(i => i.Position.Z, double.NaN, double.NaN); }
            set { foreach (Node item in Selection) item.Position = item.Position.WithZ(value); }
        }
    }
}
