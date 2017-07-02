using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Actions;
using System.Drawing;
using Salamander.Resources;
using Nucleus.Base;
using Salamander.Display;

namespace Salamander.BasicToolsGH
{
    //Disabled, as the direct use of System.Drawing causes problems when attempting to load this from within Grasshopper

    [Action("GenerateIcons")]
    class GenerateIcons : ActionBase
    {
        public override bool Execute(ExecutionInfo exInfo = null)
        {
            PrintLine("Generating Icons...");
            var commands = Core.Instance.Actions.GetCommandList();
            foreach (string command in commands)
            {
                Print(command + "...");
                Type actionType = Core.Instance.Actions.GetActionDefinition(command);
                Bitmap icon = GenerateIcon(actionType);
                if (icon != null)
                {
                    icon.Save("C:/TEMP/" + command + ".png");
                    PrintLine("Done.");
                }
                else PrintLine("No icon.");
            }
            foreach (var layer in Core.Instance.Layers.Layers)
            {
                Print(layer.Name + "...");
                Bitmap icon = GenerateBakeIcon(layer);
                if (icon != null)
                {
                    icon.Save("C:/TEMP/" + layer.Name + "_Bake.png");
                    PrintLine("Done.");
                }
                else PrintLine("No icon.");
            }
            return true;
        }

        public Bitmap GenerateIcon(Type actionType)
        {
            var actionAtt = ActionAttribute.ExtractFrom(actionType);
            if (!string.IsNullOrWhiteSpace(actionAtt.IconForeground))
            {
                if (!string.IsNullOrWhiteSpace(actionAtt.IconBackground))
                {
                    return IconResourceHelper.CombinedBitmapFromURIs(actionAtt.IconBackground, actionAtt.IconForeground);
                }
                else return IconResourceHelper.BitmapFromURI(actionAtt.IconForeground);
            }
            else if (!string.IsNullOrWhiteSpace(actionAtt.IconBackground)) return IconResourceHelper.BitmapFromURI(actionAtt.IconBackground);
            else return null;
        }

        public Bitmap GenerateIcon(DisplayLayer layer)
        {
            if (layer.IconURI != null)
                return IconResourceHelper.BitmapFromURI(layer.IconURI);
            else return null;
        }

        public Bitmap GenerateBakeIcon(DisplayLayer layer)
        {
            if (layer.IconURI != null)
                return IconResourceHelper.CombinedBitmapFromURIs(layer.IconURI, Resources.URIs.BakeIcon);
            else return null;
        }
    }
}
