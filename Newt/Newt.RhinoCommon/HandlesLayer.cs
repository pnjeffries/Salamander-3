using Rhino.DocObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using FreeBuild.Model;
using Salamander.Events;
using FreeBuild.Base;
using FreeBuild.Geometry;
using Salamander.Display;
using FreeBuild.Events;
using FreeBuild.Rendering;
using System.ComponentModel;
using RC = Rhino.Geometry;
using FreeBuild.Rhino;
using System.Timers;

namespace Salamander.Rhino
{
    public class HandlesLayer : DisplayLayer<ModelObject>
    {
        #region Fields

        /// <summary>
        /// The ID of the last rhino object to be replaced.
        /// Used to suppress handling the deletion event of this handle
        /// </summary>
        private Guid _LastReplaced = Guid.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The registry of 
        /// </summary>
        public BiDirectionary<Guid, Guid> Links
        {
            get; set;
        } = new BiDirectionary<Guid, Guid>();

        public override bool Toggleable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructors
        /// </summary>
        public HandlesLayer() : base("Handles", "The Rhino object handles which allow manipulation of model objects",0,null)
        {
            InitialiseEventWatching();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set up event handlers
        /// </summary>
        private void InitialiseEventWatching()
        {
            //Register Rhino geometry event handlers:
            RhinoDoc.DeleteRhinoObject += HandlesDeleteRhinoObject;
            RhinoDoc.UndeleteRhinoObject += HandlesUndeleteRhinoObject;
            RhinoDoc.SelectObjects += HandlesSelectObjects;
            RhinoDoc.DeselectObjects += HandlesDeselectObjects;
            RhinoDoc.DeselectAllObjects += HandlesDeselectAllObjects; ;
            RhinoDoc.ReplaceRhinoObject += HandlesReplaceRhinoObject;
            RhinoDoc.AddRhinoObject += HandlesAddRhinoObject;
            RhinoApp.Idle += HandlesIdle;
        }

        public override void InitialiseToModel(Model model)
        {
            base.InitialiseToModel(model);
            RhinoIDMappingTable idMap = Core.Instance.ActiveDocument.IDMappings.GetLatest<RhinoIDMappingTable>();
            if (idMap == null)
            {
                idMap = new RhinoIDMappingTable();
                Core.Instance.ActiveDocument.IDMappings["Rhino"] = idMap;
            }
            if (!idMap.ContainsKey(idMap.HandlesCategory))
            {
                idMap[idMap.HandlesCategory] = new BiDirectionary<Guid, Guid>();
            }
            Links = idMap[idMap.HandlesCategory];
        }

        /// <summary>
        /// Get the FreeBuild model object linked to the specified Rhino handle ID
        /// </summary>
        /// <param name="rhinoID"></param>
        /// <returns></returns>
        protected ModelObject LinkedModelObject(Guid rhinoID)
        {
            if (Links.ContainsSecond(rhinoID))
            {
                Guid objID = Links.GetFirst(rhinoID);
                return Core.Instance.ActiveDocument?.Model?.GetObject(objID);
            }
            else return null;
        }

        #endregion

        #region EventHandlers

        private void HandlesIdle(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ProcessObjectReplacedWaitingList(sender, e);
        }

        private void HandlesAddRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            if (!RhinoOutput.Writing)
            {
                if (e.TheObject.Attributes.HasUserData)
                {
                    string data = e.TheObject.Attributes.GetUserString("SAL_ORIGINAL");
                    if (!string.IsNullOrEmpty(data))
                    {
                        Guid storedGuid = new Guid(data);
                        if (this.Links.ContainsSecond(storedGuid))
                        {
                            ModelObject mO = LinkedModelObject(storedGuid);
                            VertexGeometry geometry = RCtoFB.Convert(e.TheObject.Geometry);
                            //Create copy of object:
                            Element element = null;
                            if (mO is LinearElement)
                            {
                                LinearElement lElement = ((LinearElement)mO).Duplicate();//Core.Instance.ActiveDocument?.Model?.Create.CopyOf((Element)mO, geometry);
                                if (geometry is Curve) lElement.Geometry = (Curve)geometry;
                                element = lElement;
                            }
                            if (mO is PanelElement)
                            {
                                PanelElement pElement = ((PanelElement)mO).Duplicate();//Core.Instance.ActiveDocument?.Model?.Create.CopyOf((Element)mO, geometry);
                                if (geometry is Surface) pElement.Geometry = (Surface)geometry;
                                element = pElement;
                            }
                            RhinoOutput.SetOriginalIDUserString(e.ObjectId);
                            if (element != null)
                            {
                                Links.Add(element.GUID, e.ObjectId);
                                Core.Instance.ActiveDocument.Model.Add(element);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A storage mechanism for node update operations that have been deferred to after 
        /// </summary>
        private Dictionary<Node, RhinoReplaceObjectEventArgs> _ReplacedNodesWaitingList
            = new Dictionary<Node, RhinoReplaceObjectEventArgs>();

        /// <summary>
        /// Storage for elements updated in the current batch of replacement operations
        /// </summary>
        private ElementCollection _ReplacedElements
            = new ElementCollection();

        /// <summary>
        /// The timer used to defer replacement processing operations
        /// </summary>
        //private Timer _ObjectReplacedWaitTimer = null;

        private void ProcessObjectReplacedWaitingList(object sender, EventArgs args)
        {
            if (_ReplacedElements.Count > 0 || _ReplacedNodesWaitingList.Count > 0)
            {
                foreach (KeyValuePair<Node, RhinoReplaceObjectEventArgs> kvp in _ReplacedNodesWaitingList)
                {
                    Node node = kvp.Key;
                    RC.GeometryBase geometry = kvp.Value.NewRhinoObject.Geometry;
                    if (geometry is RC.Point)
                    {
                        RC.Point rPt = (RC.Point)geometry;
                        Vector pos = RCtoFB.Convert(rPt.Location);
                        node.MoveTo(pos, true, _ReplacedElements);
                    }
                }

                _ReplacedElements.GenerateNodes(new NodeGenerationParameters());

                _ReplacedElements.Clear();
                _ReplacedNodesWaitingList.Clear();

                //_ObjectReplacedWaitTimer.Stop();

                Core.Instance.Host.Refresh();
            }
        }

        private void HandlesReplaceRhinoObject(object sender, RhinoReplaceObjectEventArgs e)
        {
            if (!RhinoOutput.Writing)
            {
                ModelObject mObj = LinkedModelObject(e.ObjectId);
                if (mObj != null)
                {
                    RC.GeometryBase geometry = e.NewRhinoObject.Geometry;
                    
                    if (mObj is Element)
                    {
                        Element element = (Element)mObj;
                        VertexGeometry vG = RCtoFB.Convert(geometry);
                        if (vG != null)
                        {
                            if (element is LinearElement && vG is Curve)
                                ((LinearElement)element).ReplaceGeometry((Curve)vG);
                            else if (element is PanelElement && vG is Surface)
                                ((PanelElement)element).ReplaceGeometry((Surface)vG);

                            _ReplacedElements.Add(element);
                        }
                    }
                    else if (mObj is Node)
                    {
                        _ReplacedNodesWaitingList[(Node)mObj] = e;
                        /*RC.GeometryBase geometry = e.NewRhinoObject.Geometry;
                        if (geometry is RC.Point)
                        {
                            Node node = (Node)mObj;
                            node.Position = RCtoFB.Convert(((RC.Point)geometry).Location);
                        }*/
                        /*if (_ObjectReplacedWaitTimer == null)
                        {
                            _ObjectReplacedWaitTimer = new Timer(100);
                            _ObjectReplacedWaitTimer.AutoReset = false;
                            _ObjectReplacedWaitTimer.Elapsed += ProcessObjectReplacedWaitingList;
                        }*/
                        //if (!_ObjectReplacedWaitTimer.Enabled)
                    }
                    //if (_ObjectReplacedWaitTimer != null) _ObjectReplacedWaitTimer.Start();
                }
            }
            _LastReplaced = e.ObjectId;
        }

        private void HandlesDeselectAllObjects(object sender, RhinoDeselectAllObjectsEventArgs e)
        {
            Core.Instance.Selected.Clear();
        }

        private void HandlesDeselectObjects(object sender, RhinoObjectSelectionEventArgs e)
        {
            if (!RhinoOutput.Writing)
            {
                foreach (RhinoObject rObj in e.RhinoObjects)
                {
                    Core.Instance.Selected.Deselect(LinkedModelObject(rObj.Id));
                }
            }
        }

        private void HandlesSelectObjects(object sender, RhinoObjectSelectionEventArgs e)
        {
            if (!RhinoOutput.Writing)
            {
                foreach (RhinoObject rObj in e.RhinoObjects)
                {
                    Core.Instance.Selected.Select(LinkedModelObject(rObj.Id));
                }
            }
        }

        private void HandlesUndeleteRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            LinkedModelObject(e.ObjectId)?.Undelete();
        }

        private void HandlesDeleteRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            if (e.ObjectId == _LastReplaced)
            {
                _LastReplaced = Guid.Empty;
            }
            else if(!RhinoOutput.Writing)
            {
                LinkedModelObject(e.ObjectId)?.Delete();
            }
        }

        public override IList<IAvatar> GenerateRepresentations(ModelObject source)
        {
            IList<IAvatar> result = new List<IAvatar>();
            if (source is Element)
                GenerateRepresentations((Element)source);   
            else if (source is Node)
                GenerateRepresentations((Node)source);
            return result;
        }

        public override IList<IAvatar> InitialRepresentation(ModelObject key)
        {
            return GenerateRepresentations(key);
        }

        /// <summary>
        /// Generate representations for nodes
        /// </summary>
        /// <param name="node"></param>
        protected void GenerateRepresentations(Node node)
        {
            if (!Links.ContainsFirst(node.GUID))
            {
                if (!node.IsDeleted)
                {
                    Guid objID = RhinoOutput.BakePoint(node.Position);
                    if (objID != Guid.Empty)
                    {
                        RhinoOutput.SetOriginalIDUserString(objID);
                        RhinoOutput.SetObjectName(objID, node.Description);
                    }
                    Links.Add(node.GUID, objID);
                }
            }
            else
            {
                Guid ptID = Links.GetSecond(node.GUID);
                if (node.IsDeleted)
                { 
                    RhinoOutput.DeleteObject(ptID);
                }
                else
                {
                    RhinoOutput.ReplacePoint(ptID, node.Position);
                }
            }
        }

        /// <summary>
        /// Generate the handle representation of an element
        /// </summary>
        /// <param name="element"></param>
        protected void GenerateRepresentations(Element element)
        {
            if (!Links.ContainsFirst(element.GUID))
            {
                if (!element.IsDeleted)
                {
                    Guid objID = Guid.Empty;
                    string idString = element.GetGeometry()?.Attributes?.SourceID;
                    if (!string.IsNullOrWhiteSpace(idString))
                    {
                        objID = new Guid(idString);
                        if (!RhinoOutput.ObjectExists(objID)) objID = Guid.Empty;
                    }
                    objID = RhinoOutput.BakeOrReplace(objID, element.GetGeometry());
                    if (objID != Guid.Empty)
                    {
                        RhinoOutput.SetOriginalIDUserString(objID);
                        RhinoOutput.SetObjectName(objID, element.Description);
                    }
                    Links.Add(element.GUID, objID);
                }
            }
            else
            {
                Guid objID = Links.GetSecond(element.GUID);
                if (element.IsDeleted)
                {
                    RhinoOutput.DeleteObject(objID);
                }
                else
                {
                    objID = RhinoOutput.BakeOrReplace(objID, element.GetGeometry());
                    Links.Set(element.GUID, objID);
                }
            }
        }

        #endregion
    }
}
