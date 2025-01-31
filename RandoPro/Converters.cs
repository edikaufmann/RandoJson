using Microsoft.Maui.Graphics.Converters;
using System.Globalization;
using RandoPro.Models;

namespace RandoPro.Converters;

public class ColorHexStringToMAUIColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string hexColor = (string)value;
        ColorTypeConverter converter = new ColorTypeConverter();

        return (Color)converter.ConvertFromInvariantString(hexColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
