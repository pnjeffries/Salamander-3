using FreeBuild.Model;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GH = Grasshopper;

namespace Salamander.Grasshopper
{
    public class GrasshopperManager
    {
        #region Properties

        /// <summary>
        /// Private static backing field for Instance property
        /// </summary>
        private static GrasshopperManager _Instance = new GrasshopperManager();

        /// <summary>
        /// The singleton instance of the Grasshopper manager class
        /// </summary>
        public static GrasshopperManager Instance
        {
            get { return _Instance; }
        }

        /// <summary>
        /// Private backing field for AutoBake property
        /// </summary>
        private bool _AutoBake = false;

        /// <summary>
        /// Auto-bake grasshopper-generated model objects into the Salamander ActiveDocument?
        /// If not, they will be placed into the background document instead.
        /// </summary>
        public bool AutoBake
        {
            get { return _AutoBake; }
            set { _AutoBake = value; }
        }

        private ModelDocument _BackgroundDocument = null;

        /// <summary>
        /// The background document that can be used to generate the model without
        /// affecting the main ActiveDocument
        /// </summary>
        public ModelDocument BackgroundDocument
        {
            get
            {
                if (_BackgroundDocument == null)
                    _BackgroundDocument = new ModelDocument();
                return _BackgroundDocument;
            }
            set
            {
                _BackgroundDocument = value;
            }
        }
        
        public void InvalidateAllSalamanderComponents(GH_Document document)
        {
            if (document != null)
            {
                foreach (IGH_DocumentObject docObj in document.Objects)
                {
                    if (docObj is SalamanderBaseComponent)
                    {
                        docObj.ExpireSolution(true);
                    }
                }
            }
        }

        #endregion
    }
}
