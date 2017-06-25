using Nucleus.Base;
using Nucleus.Model;
using Rhino;
using Rhino.FileIO;
using Rhino.PlugIns;
using Salamander.RhinoCommon;
using System;
using System.IO;
using System.Text;

namespace Salamander.RhinoPlugin
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class SalamanderPlugin : PlugIn
    {
        public SalamanderPlugin()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the NewtPlugin plug-in.</summary>
        public static SalamanderPlugin Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.

        /// <summary>
        /// Called when the plugin is being loaded.
        /// </summary>
        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // Get the version number of our plugin, that was last used, from our settings file.
            var plugin_version = Settings.GetString("PlugInVersion", null);

            if (!string.IsNullOrEmpty(plugin_version))
            {
                // If the version number of the plugin that was last used does not match the
                // version number of this plugin, proceed.
                if (0 != string.Compare(Version, plugin_version, StringComparison.OrdinalIgnoreCase))
                {
                    // Build a path to the user's staged RUI file.
                    var sb = new StringBuilder();
                    sb.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                    sb.Append("\\McNeel\\Rhinoceros\\5.0\\Plug-ins\\");
                    sb.AppendFormat("{0} ({1})", Name, Id);
                    sb.Append("\\settings\\");
                    sb.AppendFormat("{0}.rui", Assembly.GetName().Name);

                    var path = sb.ToString();

                    // Verify the RUI file exists.
                    if (File.Exists(path))
                    {
                        try
                        {
                            // Delete the RUI file.
                            File.Delete(path);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    // Save the version number of this plugin to our settings file.
                    Settings.SetString("PlugInVersion", Version);
                }
            }

            // After successfully loading the plugin, if Rhino detects a plugin RUI
            // file, it will automatically stage it, if it doesn't already exist.

            return LoadReturnCode.Success;
        }

        protected override bool ShouldCallWriteDocument(FileWriteOptions options)
        {
            return true;
        }

        protected override void WriteDocument(RhinoDoc doc, BinaryArchiveWriter archive, FileWriteOptions options)
        {
            if (Core.Instance?.ActiveDocument != null && !options.WriteSelectedObjectsOnly)
            {
                byte[] data = Core.Instance.ActiveDocument.ToBinary();

                if (data != null)
                {
                    archive.Write3dmChunkVersion(0, 1);
                    archive.WriteByteArray(data);

                    if (archive.WriteErrorOccured)
                        Core.PrintLine("ERROR: Writing Salamander model to Rhino .3dm failed!");
                    else
                        Core.PrintLine("Salamander model data written to .3dm.");
                }
            }
            base.WriteDocument(doc, archive, options);
        }

        protected override void ReadDocument(RhinoDoc doc, BinaryArchiveReader archive, FileReadOptions options)
        {
            int major, minor;
            archive.Read3dmChunkVersion(out major, out minor);
            byte[] data = archive.ReadByteArray();
            if (!archive.ReadErrorOccured && data != null)
            {
                Host.EnsureInitialisation();
                ModelDocument mDoc = Document.FromBinary<ModelDocument>(data);
                Core.Instance.ActiveDocument = mDoc;
            }
            base.ReadDocument(doc, archive, options);
        }
    }
}