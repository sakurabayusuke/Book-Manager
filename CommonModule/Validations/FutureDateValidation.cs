using System;
using System.Globalization;
using System.Windows.Controls;

namespace CommonModule.Validations
{
	public class FutureDateValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (!DateTime.TryParse((value ?? "").ToString(), CultureInfo.CurrentCulture,
				DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
				out var time)) return new ValidationResult(false, "Invalid date");

			return DateTime.Now.Date < time.Date
				? new ValidationResult(false, "Future date is Invalid")
				: ValidationResult.ValidResult;
		}
	}
}
