using System;
using System.Globalization;
using System.Windows.Data;

namespace FileSystemSimulation2.Converters
{
    public class IsNullBoolConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            return _value == null;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            throw new NotImplementedException();
        }
    }
}
