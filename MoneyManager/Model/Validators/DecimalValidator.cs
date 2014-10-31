using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoneyManager.Model.Validators
{
	public sealed class DecimalValidator : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			Decimal amount;

			string temp;
			if (string.IsNullOrWhiteSpace(temp = value as string))
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
