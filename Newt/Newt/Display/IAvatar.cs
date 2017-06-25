using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salamander.Display
{
    public interface IAvatar : IRenderable
    {
        /// <summary>
        /// The brush used to display this avatar
        /// </summary>
        DisplayBrush Brush { get; set; } 
    }
}
