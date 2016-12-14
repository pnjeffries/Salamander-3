using System;
using Rhino.PlugIns;
using Rhino.FileIO;
using Rhino;
using Salamander;
using Salamander.RhinoCommon;

namespace Salamander.Rhino.RobotImport
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class SalamanderRhinoRobotImportPlugIn : FileImportPlugIn

    {
        public SalamanderRhinoRobotImportPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the SalamanderRhinoRobotImportPlugIn plug-in.</summary>
        public static SalamanderRhinoRobotImportPlugIn Instance
        {
            get; private set;
        }

        ///<summary>Defines file extensions that this import plug-in is designed to read.</summary>
        /// <param name="options">Options that specify how to read files.</param>
        /// <returns>A list of file types that can be imported.</returns>
        protected override FileTypeList AddFileTypes(FileReadOptions options)
        {
            var result = new FileTypeList();
            result.AddFileType("Salamander: Robot File (*.rtd)", "rtd");
            return result;
        }

        /// <summary>
        /// Is called when a user requests to import a ."rtd file.
        /// It is actually up to this method to read the file itself.
        /// </summary>
        /// <param name="filename">The complete path to the new file.</param>
        /// <param name="index">The index of the file type as it had been specified by the AddFileTypes method.</param>
        /// <param name="doc">The document to be written.</param>
        /// <param name="options">Options that specify how to write file.</param>
        /// <returns>A value that defines success or a specific failure.</returns>
        protected override bool ReadFile(string filename, int index, RhinoDoc doc, FileReadOptions options)
        {
            Host.EnsureInitialisation();
            return Core.Instance.OpenDocument(filename) != null;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.
    }
}