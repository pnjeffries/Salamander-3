using Nucleus.Model;
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

        /// <summary>
        /// Private backing field for BackgroundDocuments property
        /// </summary>
        private Dictionary<Guid, ModelDocument> _BackgroundDocuments = new Dictionary<Guid, ModelDocument>();

        /// <summary>
        /// A map of Grasshopper document IDs to the background Salamander document linked to them
        /// </summary>
        public Dictionary<Guid, ModelDocument> BackgroundDocuments
        {
            get { return _BackgroundDocuments; }
        }

        /*private ModelDocument _BackgroundDocument = null;

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
        }*/

        private GrasshopperManager()
        {
           
        }

        /// <summary>
        /// Get the background Salamander document associated with the specified
        /// Grasshopper document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public ModelDocument BackgroundDocument(GH_Document document)
        {
            if (document == null) document = GH.Instances.ActiveCanvas.Document;
            if (document != null)
            {
                Guid id = document.DocumentID;
                if (!BackgroundDocuments.ContainsKey(id))
                {
                    ModelDocument doc = Core.Instance.PopulateDefaultData(new ModelDocument());
                    BackgroundDocuments.Add(id, doc);
                    return doc;
                }
                else return BackgroundDocuments[id];
            }
            else return null;
        }

        /// <summary>
        /// Expire the solution of all Salamander components in the document, forcing them to
        /// re-compute.
        /// </summary>
        /// <param name="document"></param>
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
