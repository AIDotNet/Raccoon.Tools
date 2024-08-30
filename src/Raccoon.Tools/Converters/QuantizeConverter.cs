using System.Globalization;
using Avalonia.Data.Converters;
using Raccoon.Tools.Entities;

namespace Raccoon.Tools.Converters;

public class QuantizeConverter : IValueConverter

{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is FileState state && (state == FileState.None || state == FileState.Fail))
        {
            return true;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}