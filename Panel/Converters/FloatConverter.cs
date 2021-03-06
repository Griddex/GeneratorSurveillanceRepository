﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Panel.Converters
{
    public class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().EndsWith(".") ? "." : value;
        }
    }
}
