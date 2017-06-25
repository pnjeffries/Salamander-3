using Nucleus.Base;
using Nucleus.Events;
using Nucleus.Model;
using Nucleus.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    /// <summary>
    /// Base class for display layers that contain representations of data objects
    /// </summary>
    public abstract class DisplayLayer : DisplayLayerBase, IComparable
    {
        /// <summary>
        /// The name of this display layer
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The description of this display layer
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// A value which will be used to specify the display order of this layer.
        /// Lower values will be displayed first.
        /// </summary>
        public double DisplayOrdering { get; private set; }

        /// <summary>
        /// The URI string that designates the icon to be used to represent this layer
        /// </summary>
        public string IconURI { get; private set; }

        /// <summary>
        /// A string combining the name and description of this display layer into a tooltip
        /// </summary>
        public string ToolTip
        {
            get
            {
                string result = Name;
                if (!string.IsNullOrWhiteSpace(Description)) result += " - " + Description;
                return result;
            }
        }

        /// <summary>
        /// Constructor.  Should be called from within the default constructor of any non-abstract sub-types
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <param name="description">The description of the layer</param>
        /// <param name="displayOrdering">A value which will be used to specify the display order of this layer.
        /// Lower values will be displayed first.</param>
        /// <param name="iconURI">The URI string indicating the image resource to be used as an icon for this layer.</param>
        protected DisplayLayer(string name, string description, double displayOrdering, string iconURI)
        {
            Name = name;
            Description = description;
            DisplayOrdering = displayOrdering;
            IconURI = iconURI;
        }


        /// <summary>
        /// Should be overridden.
        /// Sets up this display layer to display properties of objects in the specified model.
        /// When overriding, extract any objects to be displayed and call Register() to add them to the tracked objects
        /// </summary>
        /// <param name="doc"></param>
        public abstract void InitialiseToModel(Model model);

        /// <summary>
        /// Called whenever a new object is added to the design.
        /// The default implementation will automatically register any new object of the correct type.
        /// This should be overridden if more sophisticated filtering behaviour is required.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="e"></param>
        public abstract void OnObjectAdded(Model model, ModelObjectAddedEventArgs e);

        /// <summary>
        /// Should be overridden.
        /// A function which processes object modifications and invalidates representations as required.
        /// When implementing, check the objects modified and if appropriate call InvalidateRepresentation
        /// on any object representations that need to be updated.
        /// The default implementation simply checks whether the modified object exists as a representation key
        /// and if so invalidates it - you will probably want to override this with something more sophisticated.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="e"></param>
        public abstract void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e);

        /// <summary>
        /// Comparison implementation
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is DisplayLayer)
            {
                DisplayLayer other = (DisplayLayer)obj;
                return DisplayOrdering.CompareTo(other.DisplayOrdering);
            }
            return 0;
        }

        /// <summary>
        /// Try and register the specified object
        /// </summary>
        /// <param name="obj"></param>
        public abstract bool TryRegister(object obj);

        /// <summary>
        /// Try and register all objects in the specified collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>True if any of the items in the collection can be added</returns>
        public bool TryRegisterAll(ICollection collection)
        {
            bool result = false;
            foreach (object obj in collection)
            {
                if (TryRegister(obj)) result = true;
            }
            return result;
        }

        /// <summary>
        /// Clear all registered key objects geometry on this layer
        /// </summary>
        public abstract void Clear();
    }

    /// <summary>
    /// Generic base class for display layers that contain representations of data objects
    /// </summary>
    /// <typeparam name="T">The type of object for which this layer contains display representations</typeparam>
    public abstract class DisplayLayer<T> : DisplayLayer
    {
        /// <summary>
        /// The key objects used to store objects otherwise unassigned a key.
        /// Will usually be the default value for the data type - set to something else if that
        /// value will be used for something else.
        /// </summary>
        public T ManualObjectsKey { get; set; }

        /// <summary>
        /// Internal ornament records
        /// </summary>
        private Dictionary<T, IList<IAvatar>> _Avatars { get; } = new Dictionary<T, IList<IAvatar>>();

        /// <summary>
        /// Constructor.  Should be called from within the default constructor of any non-abstract sub-types
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <param name="description">The description of the layer</param>
        /// <param name="displayOrdering">A value which will be used to specify the display order of this layer.
        /// Lower values will be displayed first.</param>
        /// <param name="iconURI">The URI string indicating the image resource to be used as an icon for this layer.</param>
        protected DisplayLayer(string name, string description, double displayOrdering, string iconURI) : base(name, description, displayOrdering, iconURI)
        {
        }

        /// <summary>
        /// Clear all registered key objects geometry on this layer
        /// </summary>
        public override void Clear()
        {
            _Avatars.Clear();
        }

        /// <summary>
        /// Invalidate the set of display representation objects related to the specified key object
        /// These objects will be regenerated the next time they are needed.
        /// Note that if 
        /// </summary>
        /// <param name="key"></param>
        public void InvalidateRepresentation(T key)
        {
            _Avatars[key] = InitialRepresentation(key);
        }

        /// <summary>
        /// Register the specified key object for future display representation generation.
        /// </summary>
        /// <param name="key"></param>
        public void Register(T key)
        {
            _Avatars[key] = InitialRepresentation(key);
        }

        /// <summary>
        /// Try and register the specified object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool TryRegister(object obj)
        {
            if (obj != null && obj is T)
            {
                Register((T)obj);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the representation of the specified object that will be initially created
        /// when the object is first registered.  Usually this will be null and the full representation
        /// will only be created as required.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IList<IAvatar> InitialRepresentation(T key)
        {
            return null;
        }

        /// <summary>
        /// Add a new display object representing the given key value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="avatar"></param>
        public void Add(T key, IAvatar avatar)
        {
            if (!_Avatars.ContainsKey(key) || _Avatars[key] == null)
            {
                _Avatars[key] = new List<IAvatar>();
            }
            _Avatars[key].Add(avatar);
        }

        /// <summary>
        /// Add a new display object to the layer, unlinked to a key object.
        /// This object will be rendered, but because it is unlinked will not be updated automatically.
        /// It will therefore need to be removed and replaced manually.
        /// </summary>
        /// <param name="avatar"></param>
        public void Add(IAvatar avatar)
        {
            Add(ManualObjectsKey, avatar);
        }

        /// <summary>
        /// Remove a display object from this layer.
        /// </summary>
        /// <param name="avatar">The avatar to remove</param>
        /// <param name="manualOnly">If true (default), only manual (keyless) objects will be removed.  Otherwise the object will be unlinked from any keys it is found for.</param>
        public void Remove(IAvatar avatar, bool manualOnly = true)
        {
            if (manualOnly)
            {
                if (_Avatars.ContainsKey(ManualObjectsKey) && _Avatars[ManualObjectsKey] != null && _Avatars[ManualObjectsKey].Contains(avatar))
                    _Avatars[ManualObjectsKey].Remove(avatar);
            }
            else
            {
                IList<T> keyList = _Avatars.Keys.ToList<T>();
                foreach (T key in keyList)
                {
                    IList<IAvatar> reps = _Avatars[key];
                    if (reps != null && reps.Contains(avatar))
                    {
                        reps.Remove(avatar);
                    }
                }
            }
        }

        /// <summary>
        /// Should be overridden.
        /// A function in which representation objects for the specified source object will be generated.
        /// </summary>
        /// <param name="source">The source object</param>
        /// <returns></returns>
        public abstract IList<IAvatar> GenerateRepresentations(T source);

        /// <summary>
        /// Draw implementation
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override bool Draw(RenderingParameters parameters)
        {
            IList<T> keyList = _Avatars.Keys.ToList<T>();
            foreach (T key in keyList)
            {
                if (!IsHidden(key)) //Do not draw deleted or hidden objects!
                {
                    IList<IAvatar> reps = _Avatars[key];
                    if (reps == null) //This object's representation has been invalidated
                    {
                        reps = GenerateRepresentations(key);
                        _Avatars[key] = reps;
                    }
                    foreach (IAvatar av in reps)
                    {
                        av.Draw(parameters);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Whether the specified key object is hidden (either because it is invisible or deleted) and should not be drawn
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool IsHidden(T key)
        {
            if (key is IDeletable)
            {
                return ((IDeletable)(object)key).IsDeleted;
            }
            return false;
        }

        /// <summary>
        /// A function which processes object modifications and invalidates representations as required.
        /// When implementing, check the objects modified and if appropriate call InvalidateRepresentation
        /// on any object representations that need to be updated.
        /// The default implementation simply checks whether the modified object exists as a representation key
        /// and if so invalidates it - you will probably want to override this with something more sophisticated.
        /// </summary>
        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            if (modified != null && modified is T)
            {
                T modT = (T)modified;
                if (_Avatars.ContainsKey(modT)) InvalidateRepresentation(modT);
            }
        }

        /// <summary>
        /// Called whenever a new object is added to the design.
        /// The default implementation will automatically register any new object of the correct type.
        /// This should be overridden if more sophisticated behaviour is required.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="e"></param>
        public override void OnObjectAdded(Model model, ModelObjectAddedEventArgs e)
        {
            if (e.Added != null && e.Added is T)
            {
                T added = (T)(object)e.Added; //As above - necessary?
                Register(added);
            }
        }

        /// <summary>
        /// Sets up this display layer to display properties of objects in the specified model.
        /// When overriding, extract any objects to be displayed and call Register() to add them to the tracked objects.
        /// The default implementation scans through all ModelObjects in the model and will register any of the correct type.
        /// This should be overridden if more sophisticated behaviour is required.
        /// </summary>
        /// <param name="doc"></param>
        public override void InitialiseToModel(Model model)
        {
            Clear();
            foreach (object obj in model.Everything)
            {
                if (obj is T) Register((T)obj);
            }
        }

    }
}
