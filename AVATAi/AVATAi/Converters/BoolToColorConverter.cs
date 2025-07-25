using System.Globalization;

namespace AVATAi.Converters;

using System.Globalization;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isSelected = value is bool b && b;

        if (parameter is string colors && colors.Contains(","))
        {
            var parts = colors.Split(',');
            var trueColor = parts[0].Trim();
            var falseColor = parts[1].Trim();

            return isSelected ? Color.FromArgb(trueColor) : Color.FromArgb(falseColor);
        }

        return isSelected ? Colors.LightGreen : Colors.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}

