using Nucleus.Actions;
using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.BasicTools
{
    [ActionAttribute("Example", "A simple example of how to build a Salamander command action.")]
    public class ExampleAction : ActionBase
    {
        /// <summary>
        /// The first input parameter
        /// </summary>
        [ActionInput(1, "the first number to be multiplied")]
        public double First { get; set; } = 2;

        /// <summary>
        /// The second input parameter
        /// </summary>
        [ActionInput(2, "the second number to be multiplied")]
        public double Second { get; set; } = 3;

        /// <summary>
        /// The output parameter
        /// </summary>
        [ActionOutput(1, "the multiplication of the two numbers")]
        public double Answer { get; set; }

        /// <summary>
        /// Execute this action with the current input parameters
        /// </summary>
        /// <returns></returns>
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            Answer = First * Second;
            PrintLine("Answer = " + Answer);
            return true;
        }
    }
}

