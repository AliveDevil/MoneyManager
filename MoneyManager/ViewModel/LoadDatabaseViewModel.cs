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

using System.Diagnostics;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class LoadDatabaseViewModel : ViewModelBase
	{
		private RelayCommand aboutCommand;
		private RelayCommand createNewStoreCommand;
		private RelayCommand donateCommand;
		private RelayCommand openExistingStoreCommand;
		private RelayCommand<SettingsDatabaseEntryViewModel> openStoreCommand;

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

		public RelayCommand CreateNewStoreCommand
		{
			get
			{
				return createNewStoreCommand ?? (createNewStoreCommand = new RelayCommand(() =>
				{
					App.ViewState.Push<StoreCreationViewModel>();
				}));
			}
		}

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

		public IReactiveDerivedList<SettingsDatabaseEntryViewModel> Entries { get; set; }

		public RelayCommand OpenExistingStoreCommand
		{
			get
			{
				return openExistingStoreCommand ?? (openExistingStoreCommand = new RelayCommand(() =>
				{
					App.ViewState.Push<StoreLoadViewModel>();
				}));
			}
		}

		public RelayCommand<SettingsDatabaseEntryViewModel> OpenStoreCommand
		{
			get
			{
				return openStoreCommand ?? (openStoreCommand = new RelayCommand<SettingsDatabaseEntryViewModel>(databaseEntry =>
				{
					StoreViewModel storeViewModel = App.ViewState.Set<StoreViewModel>();
					storeViewModel.Store = new DatabaseContext(databaseEntry.Path, false);
				}));
			}
		}

		public LoadDatabaseViewModel()
		{
			if (InDesignMode) return;
			Entries = App.AppSettings.Databases.CreateDerivedCollection(r => new SettingsDatabaseEntryViewModel(r));
			Entries.ChangeTrackingEnabled = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue && disposing)
			{
				aboutCommand = null;
				donateCommand = null;
				createNewStoreCommand = null;
				Entries = null;
			}
			base.Dispose(disposing);
		}
	}
}
