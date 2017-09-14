using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WCF.UTILS;

namespace ProjectSwitcher.Converters
{
    public class ConfigToPropDispWrapper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //получаем обычный конфиг

            //return new PropDispNameWrapper((ProjectSwitcher.Model.Config)value);
            //return new PropDispNameWrapper((Config)value);
            if (value == null)
                return null;
            return new PropDispNameWrapper(((Config)value));    

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (new Model.Config((Config)((PropDispNameWrapper)value)?.Unwrap));
            //return DependencyProperty.UnsetValue;
        }
    }
}
