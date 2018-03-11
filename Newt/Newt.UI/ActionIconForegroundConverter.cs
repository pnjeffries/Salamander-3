using Salamander.Actions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Salamander.UI
{
    /// <summary>
    /// Converter to extract a composite icon from an action
    /// </summary>
    public class ActionIconForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IAction)
            {
                var att = ActionAttribute.ExtractFrom((IAction)value);
                return att.IconForeground;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}