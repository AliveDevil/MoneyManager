﻿/*
Copyright (C) 2015  Jöran Malek

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Windows.Controls;

namespace MoneyManager.Model.Validators
{
	public sealed class DecimalValidator : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			float amount;
			string temp = value as string;

			if (string.IsNullOrWhiteSpace(temp))
			{
				return new ValidationResult(false, Properties.Resources.InputNotEmpty);
			}
			if (!float.TryParse(temp, out amount))
			{
				return new ValidationResult(false, Properties.Resources.InputNotNumeric);
			}

			return new ValidationResult(true, amount);
		}
	}
}
