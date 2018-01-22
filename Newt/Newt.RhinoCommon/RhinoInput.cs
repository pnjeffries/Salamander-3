using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Input;
using Nucleus.Geometry;
using Nucleus.Rhino;
using Rhino.Commands;
using RC = Rhino.Geometry;
using Salamander.Rhino;
using Rhino.DocObjects;
using Nucleus.Model;

namespace Salamander.RhinoCommon
{
    /// <summary>
    /// The Rhino input controlle
    /// </summary>
    public class RhinoInput : InputController
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RhinoInput()
        {

        }

        #endregion

        #region Methods

        public override double EnterDouble(string prompt = "Enter value", double defaultValue = 0)
        {
            GetNumber gN = new GetNumber();
            gN.SetCommandPrompt(prompt);
            gN.SetDefaultNumber(defaultValue);
            if (gN.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return gN.Number();
        }

        public override int EnterInteger(string prompt = "Enter integer value", int defaultValue = 0)
        {
            GetInteger gI = new GetInteger();
            gI.SetCommandPrompt(prompt);
            gI.SetDefaultInteger(defaultValue);
            if (gI.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return gI.Number();
        }

        public override string EnterString(string prompt = "Enter string", string defaultValue = null)
        {
            GetString gS = new GetString();
            if (defaultValue != null) gS.SetDefaultString(defaultValue);
            gS.SetCommandPrompt(prompt);
            if (gS.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return gS.StringResult();
        }

        public override Vector EnterPoint(string prompt = "Enter Point")
        {
            GetPoint gP = new GetPointDynamic();
            gP.SetCommandPrompt(prompt);
            if (gP.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return RCtoN.Convert(gP.Point());
        }

        public override Vector[] EnterPoints(string prompt = "Enter points")
        {
            var result = new List<Vector>();
            
            while (true)
            {
                GetPointDynamic gP = new GetPointDynamic();
                gP.SelectionPoints = result;
                gP.SetCommandPrompt(prompt);
                gP.AcceptNothing(true);
                GetResult gR = gP.Get();
                if (gR == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
                else if (gR == GetResult.Nothing) break;
                result.Add(RCtoN.Convert(gP.Point()));
            }
            
            return result.ToArray();
        }

        public override Plane EnterPlane(string prompt = "Enter Plane")
        {
            var origin = EnterPoint(prompt);
            return new Plane(origin);
        }

        public override Vector EnterVector(string prompt = "Enter vector")
        {
            //TODO: Get direction
            GetPoint gP = new GetPoint();
            gP.SetCommandPrompt(prompt);
            if (gP.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return RCtoN.Convert(gP.Point());
        }

        public override Line EnterLine(string startPointPrompt = "Enter start of line", string endPointPrompt = "Enter end of line", Vector? startPoint = null)
        {
            /*GetLine gL = new GetLine();
            gL.FirstPointPrompt = startPointPrompt;
            gL.SecondPointPrompt = endPointPrompt;
            if (startPoint != null) gL.SetFirstPoint(FBtoRC.Convert(startPoint ?? Vector.Unset));
            RC.Line output;
            if (gL.Get(out output) == Result.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return RCtoFB.Convert(output);*/
            GetPoint gP = new GetPoint();
            gP.SetCommandPrompt(startPointPrompt);
            if (gP.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            GetPoint gP2 = new GetPointDynamic();
            gP2.SetCommandPrompt(endPointPrompt);
            gP2.SetBasePoint(gP.Point(), true);
            gP2.EnableDrawLineFromPoint(true);
            gP2.DrawLineFromPoint(gP.Point(), true);
            if (gP2.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return new Line(RCtoN.Convert(gP.Point()), RCtoN.Convert(gP2.Point()));
        }

        public override Curve EnterCurve(string prompt = "Enter curve")
        {
            return EnterGeometry(prompt, typeof(Curve)).First() as Curve;
        }

        public override VertexGeometryCollection EnterGeometry(string prompt = "Enter geometry", Type geometryType = null)
        {
            GetObject gO = new GetObject();

            if (geometryType == typeof(Curve)) gO.GeometryFilter = ObjectType.Curve;
            else if (geometryType == typeof(Surface)) gO.GeometryFilter = ObjectType.Surface | ObjectType.Mesh;
            else if (geometryType == typeof(Point)) gO.GeometryFilter = ObjectType.Point;
            else gO.GeometryFilter = ObjectType.Curve | ObjectType.Surface | ObjectType.Point | ObjectType.Mesh; //TODO: Support others

            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1, 0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            VertexGeometryCollection result = new VertexGeometryCollection();
            foreach (ObjRef objRef in gO.Objects())
            {
                try
                {
                    VertexGeometry shape = RCtoN.Convert(objRef);
                    if (shape != null) result.Add(shape);
                }
                catch (NotImplementedException)
                {
                    Core.PrintLine(objRef.Geometry()?.GetType().Name + "s not supported!");
                }
            }
            return result;
        }

        private bool FilterHandles(RhinoObject rObj, RC.GeometryBase geometry, RC.ComponentIndex index)
        {
            return Host.Instance.Handles.Links.ContainsSecond(rObj.Id);
        }

        public override Element EnterElement(string prompt = "Enter element")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            //gO.GeometryFilter = ObjectType.Curve;
            gO.SetCommandPrompt(prompt);
            if (gO.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            ObjRef rObj = gO.Object(0);
            if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
            {
                Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                return Core.Instance.ActiveDocument?.Model?.Elements[guid];
            }
            return null;
        }

        public override ElementCollection EnterElements(string prompt = "Enter elements")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            //gO.GeometryFilter = ObjectType.Curve;
            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1,0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            var result = new ElementCollection();
            foreach (ObjRef rObj in gO.Objects())
            {
                if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
                {
                    Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);

                    ElementTable elementTable = Core.Instance.ActiveDocument?.Model?.Elements;
                    if (elementTable != null && elementTable.Contains(guid))
                    {
                        Element element = Core.Instance.ActiveDocument?.Model?.Elements[guid];
                        if (element != null) result.Add(element);
                    }
                } 
            }
            return result;
        }

        public override LinearElement EnterLinearElement(string prompt = "Enter linear element")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Curve;
            gO.SetCommandPrompt(prompt);
            if (gO.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            ObjRef rObj = gO.Object(0);
            if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
            {
                Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                return Core.Instance.ActiveDocument?.Model?.Elements[guid] as LinearElement;
            }
            return null;
        }

        public override LinearElementCollection EnterLinearElements(string prompt = "Enter linear elements")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Curve;
            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1, 0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            foreach (ObjRef rObj in gO.Objects())
            {
                var result = new LinearElementCollection();
                if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
                {
                    Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                    Element element = Core.Instance.ActiveDocument?.Model?.Elements[guid];
                    if (element != null && element is LinearElement) result.Add((LinearElement)element);
                }
                return result;
            }
            return null;
        }

        public override PanelElement EnterPanelElement(string prompt = "Enter panel element")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Surface | ObjectType.Mesh;
            gO.SetCommandPrompt(prompt);
            if (gO.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            ObjRef rObj = gO.Object(0);
            if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
            {
                Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                return Core.Instance.ActiveDocument?.Model?.Elements[guid] as PanelElement;
            }
            return null;
        }

        public override PanelElementCollection EnterPanelElements(string prompt = "Enter panel elements")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Surface | ObjectType.Mesh;
            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1, 0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            foreach (ObjRef rObj in gO.Objects())
            {
                var result = new PanelElementCollection();
                if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
                {
                    Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                    Element element = Core.Instance.ActiveDocument?.Model?.Elements[guid];
                    if (element != null && element is PanelElement) result.Add((PanelElement)element);
                }
                return result;
            }
            return null;
        }

        public override Node EnterNode(string prompt = "Enter node")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Point;
            gO.SetCommandPrompt(prompt);
            if (gO.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            ObjRef rObj = gO.Object(0);
            if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
            {
                Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                return Core.Instance.ActiveDocument?.Model?.Nodes[guid] as Node;
            }
            return null;
        }

        public override NodeCollection EnterNodes(string prompt = "Enter nodes")
        {
            GetObject gO = new GetObject();
            gO.SetCustomGeometryFilter(new GetObjectGeometryFilter(FilterHandles));
            gO.GeometryFilter = ObjectType.Point;
            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1, 0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            var result = new NodeCollection();
            foreach (ObjRef rObj in gO.Objects())
            {       
                if (Host.Instance.Handles.Links.ContainsSecond(rObj.ObjectId))
                {
                    Guid guid = Host.Instance.Handles.Links.GetFirst(rObj.ObjectId);
                    Node node = Core.Instance.ActiveDocument?.Model?.Nodes[guid];
                    if (node != null && node is Node) result.Add(node);
                }
            }
            return result;
        }

        public override Direction EnterDirection(string prompt = "Enter direction", bool xyzOnly = false)
        {
            GetOption gO = new GetOption();
            gO.SetCommandPrompt(prompt);
            gO.AddOption("X");
            gO.AddOption("Y");
            gO.AddOption("Z");
            if (!xyzOnly)
            {
                gO.AddOption("XX");
                gO.AddOption("YY");
                gO.AddOption("ZZ");
            }
            // TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}
