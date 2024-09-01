using System.Globalization;
using Avalonia.Data.Converters;

namespace Raccoon.Tools.Converters;

public class ByteConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // 将byte 长度转换为可读字符串
        if (value is long size)
        {
            if (size < 1024)
            {
                return $"{size} B";
            }

            if (size < 1024 * 1024)
            {
                return $"{size / 1024.0:F2} KB";
            }

            if (size < 1024 * 1024 * 1024)
            {
                return $"{size / 1024.0 / 1024.0:F2} MB";
            }

            return $"{size / 1024.0 / 1024.0 / 1024.0:F2} GB";
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}