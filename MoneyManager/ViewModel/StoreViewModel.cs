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

using System.Data.Entity;
using System.Diagnostics;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public enum StoreMode { Overview, Budget, Report }

	public class StoreViewModel : ViewModelBase
	{
		private IReactiveDerivedList<AccountInfoViewModel> accounts;
		private RelayCommand addAccountCommand;
		private RelayCommand donateCommand;
		private RelayCommand<AccountInfoViewModel> selectAccountCommand;
		private RelayCommand<StoreMode> selectViewMode;
		private DatabaseContext store;

		public IReactiveDerivedList<AccountInfoViewModel> Accounts
		{
			get
			{
				return accounts;
			}
			set
			{
				if (accounts == value) return;
				accounts = value;
				OnPropertyChanged(nameof(Accounts));
			}
		}

		public RelayCommand AddAccountCommand
		{
			get
			{
				return addAccountCommand ?? (addAccountCommand = new RelayCommand(() =>
				{
					store.AccountSet.Add(store.AccountSet.Create());
					store.SaveChanges();
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

		public RelayCommand<AccountInfoViewModel> SelectAccountCommand
		{
			get
			{
				return selectAccountCommand ?? (selectAccountCommand = new RelayCommand<AccountInfoViewModel>(accountInfo =>
				{
					ViewState.Set(new AccountDetailViewModel((Account)accountInfo, this));
				}));
			}
		}

		public RelayCommand<StoreMode> SelectViewMode
		{
			get
			{
				return selectViewMode ?? (selectViewMode = new RelayCommand<StoreMode>(mode =>
				{
					switch (mode)
					{
						case StoreMode.Overview:
							break;

						case StoreMode.Budget:
							break;

						case StoreMode.Report:
							break;

						default:
							break;
					}
				}));
			}
		}

		public DatabaseContext Store
		{
			get
			{
				return store;
			}
			set
			{
				if (InDesignMode) return;
				if (store == value) return;
				store = value;
				OnPropertyChanged(nameof(Store));
				Accounts = store.AccountSet.Local.CreateDerivedCollection(a => new AccountInfoViewModel(a, this));
				Accounts.Reset();
			}
		}

		public ViewStateManager ViewState { get; } = new ViewStateManager();

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue && disposing)
			{
				ViewState.Clear();
				store?.SaveChanges();
				store?.Dispose();
				store = null;
			}
			base.Dispose(disposing);
		}
	}
}
