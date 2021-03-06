﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Panel.Converters
{
    public class ConvertDateTimeToTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                DateTime genStartedTime = (DateTime)value;
                return genStartedTime.ToString("hh:mm:ss tt");
            }
            return null; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
