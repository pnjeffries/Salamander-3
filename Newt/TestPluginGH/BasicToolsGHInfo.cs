using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace TestPluginGH
{
    public class TestPluginGHInfo : GH_AssemblyInfo
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
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("42E44C6C-0623-4FCA-9CB5-435672141F17");
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
                return "paul.jeffries@ramboll.co.uk";
            }
        }
    }
}
