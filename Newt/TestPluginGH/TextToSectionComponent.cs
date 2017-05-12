using Salamander.Grasshopper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace Salamander.BasicToolsGH
{
    public class TextToSectionComponent : SalamanderBaseComponent
    {
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("{8741D3E0-CBF0-4C61-A112-840E2126D461}");
            }
        }

        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }

        public TextToSectionComponent()
            : base("TextToSection", "Text To Section", "Text2Sect", SubCategories.Sections)
        { }
    }
}
