using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Salamander.Grasshopper
{
    public class GrasshopperInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Salamander 3";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return IconResourceHelper.BitmapFromURI(IconResourceHelper.ResourceLocation + "Salamander3_64x64.png");
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Structual Analysis Link And MANager for Data Entry & Retrieval";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("92fa4d82-5780-40ac-bb9a-16dff2f3b09c");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Paul Jeffries";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
