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

namespace Salamander.Rhino
{
    public class HandlesManager : DisplayLayer<ModelObject>
    {
        #region Properties

        /// <summary>
        /// The registry of 
        /// </summary>
        public BiDirectionary<Guid, Guid> Links
        {
            get; set;
        } = new BiDirectionary<Guid, Guid>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructors
        /// </summary>
        public HandlesManager() : base("Handles", "The Rhino object handles which allow manipulation of model objects",0,null)
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
                            if (mO is LinearElement)
                            {
                                LinearElement newElement = ((LinearElement)mO).Duplicate();//Core.Instance.ActiveDocument?.Model?.Create.CopyOf((Element)mO, geometry);
                                if (geometry is Curve) newElement.Geometry = (Curve)geometry;
                                RhinoOutput.SetOriginalIDUserString(e.ObjectId);
                                Links.Add(newElement.GUID, e.ObjectId);
                                Core.Instance.ActiveDocument.Model.Add(newElement);
                            }
                        }
                    }
                }
            }
        }

        private void HandlesReplaceRhinoObject(object sender, RhinoReplaceObjectEventArgs e)
        {
            if (!RhinoOutput.Writing)
            {
                ModelObject mObj = LinkedModelObject(e.ObjectId);
                if (mObj != null)
                {
                    if (mObj is LinearElement)
                    {
                        RC.GeometryBase geometry = e.NewRhinoObject.Geometry;
                        if (geometry is RC.Curve)
                        {
                            Curve crv = RCtoFB.Convert((RC.Curve)geometry);
                            if (crv != null)
                                ((LinearElement)mObj).ReplaceGeometry(crv);
                        }
                    }
                }
            }
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
            if (!RhinoOutput.Writing)
            {
                LinkedModelObject(e.ObjectId)?.Delete();
            }
        }

        public override IList<IAvatar> GenerateRepresentations(ModelObject source)
        {
            IList<IAvatar> result = new List<IAvatar>();
            if (source is LinearElement)
            {
                GenerateRepresentations((LinearElement)source);   
            }
            return result;
        }

        public override IList<IAvatar> InitialRepresentation(ModelObject key)
        {
            return GenerateRepresentations(key);
        }

        protected void GenerateRepresentations(LinearElement element)
        {
            if (!Links.ContainsFirst(element.GUID))
            {
                if (!element.IsDeleted)
                {

                    Guid objID = Guid.Empty;
                    string idString = element.Geometry?.Attributes?.SourceID;
                    if (!string.IsNullOrWhiteSpace(idString))
                    {
                        objID = new Guid(idString);
                        if (!RhinoOutput.ObjectExists(objID)) objID = Guid.Empty;
                    }
                    objID = RhinoOutput.BakeOrReplace(objID, element.Geometry);
                    if (objID != Guid.Empty) RhinoOutput.SetOriginalIDUserString(objID);
                    Links.Add(element.GUID, objID);
                }
            }
            else
            {
                Guid curveID = Links.GetSecond(element.GUID);
                if (element.IsDeleted)
                {
                    RhinoOutput.DeleteObject(curveID);
                    //Links.Remove(element.GUID);
                }
                else
                {
                    RhinoOutput.ReplaceCurve(curveID, element.Geometry);
                }
            }
        }

        #endregion
    }
}
