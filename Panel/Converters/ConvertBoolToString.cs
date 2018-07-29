﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Panel.Converters
{
    [ValueConversion(typeof(Boolean),typeof(String))]
    public class ConvertBoolToString : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            bool isNull = (bool)value;
            if (isNull)
                return $"ON";
            else
                return "OFF";
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
