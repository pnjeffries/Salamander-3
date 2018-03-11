using Nucleus.Model;
using Nucleus.Model.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Selection
{
    public class ResultsCaseDummy : ResultsCase
    {
        public ResultsCaseDummy(string name) : base(name) { }

        public override bool Contains(Load load)
        {
            return true;
        }
    }
}
