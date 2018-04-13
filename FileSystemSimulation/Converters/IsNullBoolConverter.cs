using System;
using System.Globalization;
using System.Windows.Data;

namespace FileSystemSimulation.Converters
{
    public class IsNullBoolConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            if (_value == null)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            throw new NotImplementedException();
        }
    }
}
