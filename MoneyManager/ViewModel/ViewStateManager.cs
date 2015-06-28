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
using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyManager.ViewModel
{
	public class ViewStateManager : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ViewModelBase view;
		private Stack<ViewModelBase> viewStack = new Stack<ViewModelBase>();

		public ViewModelBase View
		{
			get
			{
				return view;
			}
			set
			{
				if (view == value) return;
				view = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(View)));
			}
		}

		public ViewStateManager()
		{
		}

		public void Pop()
		{
			if (viewStack.Count == 0) return;
			ViewModelBase viewModel = viewStack.Pop();
			UpdateView();
			viewModel.Dispose();
		}

		public T Push<T>(T t) where T : ViewModelBase
		{
			viewStack.Push(t);
			UpdateView();
			return t;
		}

		public T Push<T>() where T : ViewModelBase, new()
		{
			return Push(new T());
		}

		public T Set<T>(T t) where T : ViewModelBase
		{
			Clear();
			return Push(t);
		}

		public T Set<T>() where T : ViewModelBase, new()
		{
			return Set(new T());
		}

		public void Clear()
		{
			View = null;
			while(viewStack.Count > 0)
			{
				viewStack.Pop().Dispose();
			}
		}

		private void UpdateView()
		{
			View = viewStack.Count > 0 ? viewStack.Peek() : null;
		}
	}
}
