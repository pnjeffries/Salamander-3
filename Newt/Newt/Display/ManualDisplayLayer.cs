using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Model;

namespace Salamander.Display
{
    /// <summary>
    /// Display layer which can be used to manually create and display preview geometry for actions
    /// </summary>
    public class ManualDisplayLayer : DisplayLayer<object>
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public ManualDisplayLayer() : base("Preview", "Preview display layer", 1000, null)
        {
            ManualObjectsKey = "Default";
        }

        public override IList<IAvatar> GenerateRepresentations(object source)
        {
            return null;
        }

        public override void InitialiseToModel(Model model)
        {
           //Do nothing!
        }
    }
}
