using Rhino.DocObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using FreeBuild.Model;
using Newt.Events;
using FreeBuild.Base;
using Newt.Display;
using FreeBuild.Events;
using FreeBuild.Rendering;
using System.ComponentModel;

namespace Newt.Rhino
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

        #endregion

        #region EventHandlers

        private void HandlesIdle(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesAddRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesReplaceRhinoObject(object sender, RhinoReplaceObjectEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesDeselectAllObjects(object sender, RhinoDeselectAllObjectsEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesDeselectObjects(object sender, RhinoObjectSelectionEventArgs e)
        {
            ///throw new NotImplementedException();
        }

        private void HandlesSelectObjects(object sender, RhinoObjectSelectionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesUndeleteRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandlesDeleteRhinoObject(object sender, RhinoObjectEventArgs e)
        {
            //throw new NotImplementedException();
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
                    Guid curveID = RhinoOutput.BakeCurve(element.Geometry);
                    Links.Add(element.GUID, curveID);
                }
            }
            else
            {
                Guid curveID = Links.GetSecond(element.GUID);
                if (element.IsDeleted)
                {
                    RhinoOutput.DeleteObject(curveID);
                    Links.Remove(element.GUID);
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
