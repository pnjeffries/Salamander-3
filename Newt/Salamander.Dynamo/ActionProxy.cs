using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Dynamo
{
    public class ActionProxy
    {
        public static Dictionary<string,object> ExecuteAction(string commandName, params object[] inputs)
        {
            //TODO: Convert inputs
            IAction action = Core.Instance.Actions.ExecuteActionWithInputs(commandName, inputs);
            if (action != null)
            {
                return action.GetOutputsDictionary(); //TODO: Convert outputs
            }
            else return null;
        }
    }
}
