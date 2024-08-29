using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace Raccoon.Tools.Converters;

public class UseAvatarConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is Dictionary<string, string> dict && dict.TryGetValue("avatar", out var convert))
        {
            return convert;
        }
        
        return new Bitmap("images/avatar.jpg");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}