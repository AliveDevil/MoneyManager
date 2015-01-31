using System;
using System.Windows.Controls;

namespace MoneyManager.Model.Validators
{
	public sealed class DecimalValidator : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			Decimal amount;
			string temp = value as string;

			if (string.IsNullOrWhiteSpace(temp))
			{
				return new ValidationResult(false, Properties.Resources.InputNotEmpty);
			}
			if (!Decimal.TryParse(temp, out amount))
			{
				return new ValidationResult(false, Properties.Resources.InputNotNumeric);
			}

			return new ValidationResult(true, amount);
		}
	}
}
