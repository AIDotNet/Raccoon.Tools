using System.Globalization;
using Avalonia.Data.Converters;

namespace Raccoon.Tools.Converters;

public class UserBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && str.Equals("user", StringComparison.OrdinalIgnoreCase))
        {
            return "Transparent";
        }

        return "Transparent";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}