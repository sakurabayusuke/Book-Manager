﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace CommonModule.Validations
{
	public class DateValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return !DateTime.TryParse((value ?? "").ToString(),
				CultureInfo.CurrentCulture,
				DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
				out _) ? ValidationResult.ValidResult
				: new ValidationResult(false, "Invalid date");
		}
	}
}
