using System;
using System.Globalization;
using System.Windows.Data;

namespace CommonModule.Converter
{
	public class ReverseBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is not true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is not true;
		}
	}
}
