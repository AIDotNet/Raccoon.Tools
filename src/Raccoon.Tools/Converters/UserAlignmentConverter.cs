using System.Globalization;
using Avalonia.Data.Converters;

namespace Raccoon.Tools.Converters;

public class UserAlignmentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && str.Equals("user", StringComparison.OrdinalIgnoreCase))
        {
            return "Right";
        }

        return "Left";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}