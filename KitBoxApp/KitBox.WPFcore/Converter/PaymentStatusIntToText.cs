﻿using KitBox.Core.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KitBox.WPFcore.Converter
{
    public class PaymentStatusIntToText : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, CultureInfo language)
        {
            string result;
            try
            {
                result = status.ResourceManager.GetString(nameof(PaymentStatus) + "_" + ((PaymentStatus)value).ToString());
            }
            catch
            {
                result = ((PaymentStatus)value).ToString();
            }
            return  result;
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }
}
