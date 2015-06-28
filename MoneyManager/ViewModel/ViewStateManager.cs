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

		private static Lazy<ViewStateManager> singleton = new Lazy<ViewStateManager>(() => new ViewStateManager());
		private ViewModelBase view;
		private Stack<ViewModelBase> viewStack = new Stack<ViewModelBase>();

		public static ViewStateManager Singleton { get { return singleton.Value; } }

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

		private ViewStateManager()
		{
			Set<LoadDatabaseViewModel>();
		}

		public void Pop()
		{
			if (viewStack.Count == 0) return;
			ViewModelBase viewModel = viewStack.Pop();
			UpdateView();
			viewModel.Dispose();
		}

		public T Push<T>() where T : ViewModelBase, new()
		{
			T t = new T();
			viewStack.Push(t);
			UpdateView();
			return t;
		}

		public T Set<T>() where T : ViewModelBase, new()
		{
			View = null;
			while (viewStack.Count > 0)
			{
				ViewModelBase viewModel = viewStack.Pop();
				viewModel.Dispose();
			}
			return Push<T>();
		}

		private void UpdateView()
		{
			View = viewStack.Peek();
		}
	}
}
