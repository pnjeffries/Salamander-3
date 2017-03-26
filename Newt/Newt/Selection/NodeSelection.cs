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

        /// <summary>
        /// Fixity.X
        /// </summary>
        public bool? Fixity_X
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.X, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithX((bool)value);
                    }
            }
        }

        /// <summary>
        /// Fixity.Y
        /// </summary>
        public bool? Fixity_Y
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.Y, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithY((bool)value);
                    }
            }
        }

        /// <summary>
        /// Fixity.Z
        /// </summary>
        public bool? Fixity_Z
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.Z, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithZ((bool)value);
                    }
            }
        }

        /// <summary>
        /// Fixity.XX
        /// </summary>
        public bool? Fixity_XX
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.XX, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithXX((bool)value);
                    }
            }
        }

        /// <summary>
        /// Fixity.YY
        /// </summary>
        public bool? Fixity_YY
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.YY, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithYY((bool)value);
                    }
            }
        }

        /// <summary>
        /// Fixity.ZZ
        /// </summary>
        public bool? Fixity_ZZ
        {
            get { return CombinedValue<bool?>(i => i.GetData<NodeSupport>().Fixity.ZZ, null, false); }
            set
            {
                if (value.HasValue)
                    foreach (Node item in Selection)
                    {
                        NodeSupport support = item.GetData<NodeSupport>(true);
                        support.Fixity = support.Fixity.WithZZ((bool)value);
                    }
            }
        }

    
    }
}
