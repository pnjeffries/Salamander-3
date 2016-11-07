using FreeBuild.Events;
using FreeBuild.Extensions;
using FreeBuild.Model;
using FreeBuild.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Newt.Display
{
    /// <summary>
    /// Manager class for display layers.
    /// Deals with loading display plugins and maintaining and manipulating display layers
    /// </summary>
    public class DisplayLayerManager
    {
        /// <summary>
        /// The collection of layers
        /// </summary>
        public List<DisplayLayer> Layers { get; set; } = new List<DisplayLayer>();

        /// <summary>
        /// Constructor
        /// </summary>
        public DisplayLayerManager()
        {

        }

        /// <summary>
        /// Load up all layer types from an assembly and add them to the layer table
        /// </summary>
        /// <param name="pluginAss"></param>
        /// <returns>True if the specified assembly</returns>
        public bool LoadPlugin(Assembly pluginAss)
        {
            bool result = false;
            Type[] types = pluginAss.GetTypes();
            foreach (Type type in types)
            {
                if (LoadLayer(type)) result = true;
            }
            Layers.Sort();
            return result;
        }

        /// <summary>
        /// Load the specified layer type into the layer table.
        /// </summary>
        /// <param name="layerType">The type of layer to load.  Must derive from DisplayLayer.</param>
        /// <returns></returns>
        public bool LoadLayer(Type layerType)
        {
            if (typeof(DisplayLayer).IsAssignableFrom(layerType) && !layerType.IsAbstract)
            {
                if (!Layers.ContainsType(layerType))
                {
                    DisplayLayer dLayer = (DisplayLayer)Activator.CreateInstance(layerType);
                    Layers.Add(dLayer);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Register a model with the layer system
        /// </summary>
        /// <param name="model"></param>
        public void RegisterModel(Model model)
        {
            foreach (DisplayLayer layer in Layers)
            {
                layer.InitialiseToModel(model);
            }
            model.ObjectPropertyChanged += HandleModelObjectPropertyChanged;
            model.ObjectAdded += HandleModelObjectAdded;
        }

        /// <summary>
        /// Handle a property change on a tracked model object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void HandleModelObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (DisplayLayer layer in Layers)
            { 
                layer.InvalidateOnUpdate(sender, e);
            }
        }

        /// <summary>
        /// Handle a new object being added to a tracked model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleModelObjectAdded(object sender, ModelObjectAddedEventArgs e)
        {
            foreach (DisplayLayer layer in Layers)
            {
                layer.OnObjectAdded((Model)sender, e);
            }
        }

        /// <summary>
        /// Draw all visible layers
        /// </summary>
        /// <param name="parameters"></param>
        public void Draw(RenderingParameters parameters)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                DisplayLayer layer = Layers[i];
                if (layer.Visible)
                {
                    layer.Draw(parameters);
                }
            }
        }
    }
}
