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

using System.Diagnostics;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class LoadDatabaseViewModel : ViewModelBase
	{
		private RelayCommand createNewStoreCommand;
		private RelayCommand aboutCommand;

		public RelayCommand CreateNewStoreCommand
		{
			get
			{
				return createNewStoreCommand ?? (createNewStoreCommand = new RelayCommand(() =>
				{
					ViewStateManager.Singleton.Push<StoreCreationViewModel>();
				}));
			}
		}

		public RelayCommand AboutCommand
		{
			get
			{
				return aboutCommand ?? (aboutCommand = new RelayCommand(() =>
				{
					ViewStateManager.Singleton.Push<AboutViewModel>();
				}));
			}
		}

		public IReactiveDerivedList<SettingsDatabaseEntryViewModel> Entries { get; set; }

		private RelayCommand<SettingsDatabaseEntryViewModel> openStoreCommand;

		public RelayCommand<SettingsDatabaseEntryViewModel> OpenStoreCommand
		{
			get
			{
				return openStoreCommand ?? (openStoreCommand = new RelayCommand<SettingsDatabaseEntryViewModel>(databaseEntry =>
				{
					StoreViewModel storeViewModel = ViewStateManager.Singleton.Set<StoreViewModel>();
					storeViewModel.Store = new DatabaseContext(databaseEntry.Path, false);
				}));
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


		public LoadDatabaseViewModel()
		{
			if (!InDesignMode)
			{
				Entries = App.AppSettings.Databases.CreateDerivedCollection(r => new SettingsDatabaseEntryViewModel(r));
				Entries.ChangeTrackingEnabled = true;
			}
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
