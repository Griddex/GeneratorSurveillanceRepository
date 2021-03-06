﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Panel.Converters
{
    public class ConvertTrueOrFalseToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value != null)
                {
                    bool booleanvalue = (bool)value;
                    if (booleanvalue)
                        return "Collapsed";
                    else
                        return "Visible";
                }
            }
            catch (Exception) { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
