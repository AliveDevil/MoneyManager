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
using System.Diagnostics;
using Microsoft.Win32;
using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class StoreCreationViewModel : ViewModelBase
	{
		private RelayCommand aboutCommand;
		private RelayCommand backCommand;
		private string name;
		private string path;
		private RelayCommand selectStoreLocationCommand;
		private RelayCommand createStoreCommand;

		public RelayCommand AboutCommand
		{
			get
			{
				return aboutCommand ?? (aboutCommand = new RelayCommand(() =>
				{
					App.ViewState.Push<AboutViewModel>();
				}));
			}
		}

		public RelayCommand BackCommand
		{
			get
			{
				return backCommand ?? (backCommand = new RelayCommand(() =>
				{
					App.ViewState.Pop();
				}));
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (name == value) return;
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Path
		{
			get
			{
				return path;
			}
			set
			{
				if (path == value) return;
				path = value;
				OnPropertyChanged(nameof(Path));
			}
		}

		private RelayCommand donateCommand;

		public RelayCommand DonateCommand
		{
			get
			{
				return donateCommand ?? (donateCommand = new RelayCommand(() =>
				{
					Process.Start("http://donation-tracker.com/u/alivedevil");
				}));
			}
		}

		public RelayCommand CreateStoreCommand
		{
			get
			{
				return createStoreCommand ?? (createStoreCommand = new RelayCommand(() =>
				{
					SettingsDatabaseEntry databaseEntry = App.AppSettings.NewEntry(name, path);
					DatabaseContext context = new DatabaseContext(databaseEntry.Path, true);
					StoreViewModel store = App.ViewState.Set<StoreViewModel>();
					store.Store = context;
				}));
			}
		}

		public RelayCommand SelectStoreLocationCommand
		{
			get
			{
				return selectStoreLocationCommand ?? (selectStoreLocationCommand = new RelayCommand(() =>
				{
					SaveFileDialog dialog = new SaveFileDialog()
					{
						AddExtension = true,
						CheckPathExists = true,
						CreatePrompt = false,
						DereferenceLinks = true,
						FileName = "New Store.store",
						Filter = "MoneyManager Store|*.store",
						InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
						OverwritePrompt = true,
						RestoreDirectory = true,
						Title = "Select Store location"
					};
					if (dialog.ShowDialog() == true) Path = dialog.FileName;
				}));
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue && disposing)
			{
				aboutCommand = null;
				backCommand = null;
				selectStoreLocationCommand = null;
				createStoreCommand = null;
			}
			base.Dispose(disposing);
		}
	}
}
