using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoneyManager.Model.Validators
{
	public class UniqueKeyValidator : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			string stringValue = value as string;
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return new ValidationResult(false, null);
			}

			if (DatabaseContext.Instance.TagSet.Any(tag => tag.Key.Equals(stringValue)))
			{
				return new ValidationResult(false, null);
			}
			return ValidationResult.ValidResult;
		}
	}
}
