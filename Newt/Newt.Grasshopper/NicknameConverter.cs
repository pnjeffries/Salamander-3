using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Grasshopper
{
    /// <summary>
    /// Class that can be used to extract a unique nickname from a parameter name
    /// </summary>
    public class NicknameConverter
    {
        /// <summary>
        /// The previous nicknames extracted 
        /// </summary>
        protected IList<string> Previous { get; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public NicknameConverter()
        {

        }

        /// <summary>
        /// Convert a full name to a shorter nickname
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public string Convert(string fullName)
        {
            string result = null;
            if (fullName.Length > 0)
            {
                result = fullName.First().ToString();
            }
            if (result == null) result = "A";
            if (Previous.Contains(result))
            {
                //TODO
            }
            Previous.Add(result);
            return result;
        }
    }
}
