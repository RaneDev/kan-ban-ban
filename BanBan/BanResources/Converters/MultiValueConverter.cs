using System.Windows.Data;
using System;
using System.Globalization;

namespace BanResources.Converters
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length switch
            {
                1 => (values[0]),
                2 => (values[0], values[1]),
                3 => (values[0], values[1], values[2]),
                4 => (values[0], values[1], values[2], values[3]),
                5 => (values[0], values[1], values[2], values[3], values[4]),
                6 => (values[0], values[1], values[2], values[3], values[4], values[5]),
                7 => (values[0], values[1], values[2], values[3], values[4], values[5], values[6]),
                8 => (values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]),
                _ => throw new ArgumentException("Undefined number of arguments"),
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
