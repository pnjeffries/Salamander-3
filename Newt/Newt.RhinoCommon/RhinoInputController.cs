using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Input;
using FreeBuild.Geometry;
using FreeBuild.Rhino;
using Rhino.Commands;
using RC = Rhino.Geometry;
using Salamander.Rhino;
using Rhino.DocObjects;

namespace Salamander.RhinoCommon
{
    /// <summary>
    /// The Rhino input controlle
    /// </summary>
    public class RhinoInputController : InputController
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public RhinoInputController()
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

        public override string EnterString(string prompt = "Enter string", string defaultValue = "")
        {
            GetString gS = new GetString();
            gS.SetCommandPrompt(prompt);
            if (gS.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return gS.StringResult();
        }

        public override Vector EnterPoint(string prompt = "Enter Point")
        {
            GetPoint gP = new GetPoint();
            gP.SetCommandPrompt(prompt);
            if (gP.Get() == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            return RCtoFB.Convert(gP.Point());
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
            return new Line(RCtoFB.Convert(gP.Point()), RCtoFB.Convert(gP2.Point()));
        }

        public override ShapeCollection EnterGeometry(string prompt = "Enter geometry")
        {
            GetObject gO = new GetObject();
            gO.GeometryFilter = ObjectType.Curve; //TODO
            gO.SetCommandPrompt(prompt);
            if (gO.GetMultiple(1, 0) == GetResult.Cancel) throw new OperationCanceledException("Operation cancelled by user");
            ShapeCollection result = new ShapeCollection();
            foreach (ObjRef objRef in gO.Objects())
            {
                try
                {
                    Shape shape = RCtoFB.Convert(objRef);
                    if (shape != null) result.Add(shape);
                }
                catch (NotImplementedException)
                {
                    Core.PrintLine(objRef.Geometry()?.GetType().Name + "s not supported!");
                }
            }
            return result;
        }

        #endregion
    }
}
