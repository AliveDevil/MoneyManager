/*
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

using System;
using System.Windows.Input;

namespace MoneyManager.ViewModel
{
	public class RelayCommand<T> : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private Func<T, bool> canExecute;
		private Action<T> execute;

		public RelayCommand(Action<T> executeAction) : this(executeAction, null)
		{
		}

		public RelayCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc)
		{
			execute = executeAction;
			canExecute = canExecuteFunc;
		}

		public bool CanExecute(object parameter)
		{
			if (parameter != null && !(parameter is T))
				throw new ArgumentException();
			return canExecute?.Invoke((T)parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			if (parameter != null && !(parameter is T))
				throw new ArgumentException();
			execute((T)parameter);
		}
	}
}
