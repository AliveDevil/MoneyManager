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

using System.Windows;
using System.Windows.Input;

namespace MoneyManager.Views
{
	public class AddToInputBinding
	{
		public static readonly DependencyProperty BindingProperty =
			DependencyProperty.RegisterAttached("Binding", typeof(InputBinding), typeof(AddToInputBinding), new PropertyMetadata((s, e) =>
			{
				((UIElement)s).InputBindings.Add((InputBinding)e.NewValue);
			}));

		public static InputBinding GetBinding(DependencyObject obj)
		{
			return (InputBinding)obj.GetValue(BindingProperty);
		}

		public static void SetBinding(DependencyObject obj, InputBinding value)
		{
			obj.SetValue(BindingProperty, value);
		}
	}
}
