using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using Nucleus.Model;
using Nucleus.Physics;
using Nucleus.Geometry;

namespace Salamander.BasicTools
{
    [Action("Relax", 
        "Perform a simple dynamic relaxation analysis on a set of linear elements modelled as pin-ended struts and ties.",
        IconBackground = Resources.URIs.Relax)]
    public class Relax : ModelActionBase
    {
        [ActionInput(1, "the Strut Elements", Required = false, OneByOne = false)]
        public LinearElementCollection Struts { get; set; } = new LinearElementCollection();

        [ActionInput(2, "the Tie (tension only) Elements", Required = false, OneByOne = false)]
        public LinearElementCollection Ties { get; set; } = new LinearElementCollection();

        [ActionInput(3, "the maximum number of cycles to run")]
        public int Cycles { get; set; } = 100;

        /*[ActionInput(4, "the maximum distance (in m) any particle can move in one time-step.  " + 
            "This can be reduced to avoid model instability (though the number of cycles should be increased to compensate)")]
        public double SpeedLimit { get; set; } = 0.1;*/

        [ActionOutput(1, "the deformed strut geometry")]
        public CurveCollection StrutDeformation { get; set; }

        [ActionOutput(2, "the deformed tie geometry")]
        public CurveCollection TieDeformation { get; set; }

        [ActionOutput(3, "the axial forces in the strut members due to deformation")]
        public List<double> StrutForces { get; set; }

        [ActionOutput(4, "the axial forces in the tie members due to deformation")]
        public List<double> TieForces { get; set; }

        public override bool Execute(ExecutionInfo exInfo = null)
        {
            var elements = new LinearElementCollection();
            if (Struts != null) elements.AddRange(Struts);
            if (Ties != null) elements.AddRange(Ties);

            if (elements.Count > 0)
            {
                elements.ClearAttachedData(typeof(Spring));
                var nodes = elements.GetNodes();
                nodes.ClearAttachedData(typeof(Particle));

                var motion = new KineticDampingComponent(nodes);
                //motion.SpeedLimit = SpeedLimit;
                var gravity = new ParticleGravityComponent(nodes);
                var springForces = new SpringForceComponent(elements);

                if (Struts != null)
                    foreach (var strut in Struts)
                    {
                        if (strut.HasData<Spring>())
                        {
                            strut.GetData<Spring>().Compression = true;
                            strut.GetData<Spring>().Tension = true;
                        }
                    }

                //Set ties (temp):
                if (Ties != null)
                    foreach (var tie in Ties)
                    {
                        if (tie.HasData<Spring>())
                        {
                            tie.GetData<Spring>().Compression = false;
                            tie.GetData<Spring>().Tension = true;
                        }
                    }

                var engine = new PhysicsEngine();
                engine.Components.Add(gravity);
                engine.Components.Add(springForces);
                engine.Components.Add(motion);

                engine.Start();
                for (int i = 0; i < Cycles; i++)
                {
                    engine.Cycle(1.0);
                }

                // Outputs:
                StrutDeformation = new CurveCollection();
                TieDeformation = new CurveCollection();
                StrutForces = new List<double>();
                TieForces = new List<double>();

                if (Struts != null)
                    foreach (var lEl in Struts)
                    {
                        if (!lEl.IsDeleted)
                        {
                            StrutDeformation.Add(
                                new Line(
                                    lEl.StartNode.GetData<Particle>().Position,
                                    lEl.EndNode.GetData<Particle>().Position));

                            StrutForces.Add(
                                lEl.GetData<Spring>().AxialForce());
                        }
                    }

                if (Ties != null)
                    foreach (var lEl in Ties)
                    {
                        if (!lEl.IsDeleted)
                        {
                            TieDeformation.Add(
                            new Line(
                                lEl.StartNode.GetData<Particle>().Position,
                                lEl.EndNode.GetData<Particle>().Position));

                            TieForces.Add(
                                lEl.GetData<Spring>().AxialForce());
                        }
                    }
            }

            return true;
        }
    }
}
