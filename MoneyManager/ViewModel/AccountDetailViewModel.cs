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

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class AccountDetailViewModel : StoreViewModelBase
	{
		private Account account;
		private RelayCommand applyRecordCommand;
		private RelayCommand deleteRecordCommand;
		private RelayCommand discardRecordCommand;
		private RelayCommand editCommand;
		private RecordInfoViewModel selectedRecord;

		public RelayCommand ApplyRecordCommand
		{
			get
			{
				return applyRecordCommand ?? (applyRecordCommand = new RelayCommand(() =>
				{
					Record record = (Record)selectedRecord;
					DbEntityEntry<Record> recordEntry = Store.Entry(record);
					if (recordEntry.State == EntityState.Detached)
					{
						recordEntry.Entity.Account = account;
						Store.RecordSet.Local.Add(record);
					}
					Store.SaveChanges();
					AssignNewRecord();
				}));
			}
		}

		private RelayCommand addRecordCommand;

		public RelayCommand AddRecordCommand
		{
			get
			{
				return addRecordCommand ?? (addRecordCommand = new RelayCommand(() =>
				{
					ViewState.Push(new RecordAddViewModel(account, StoreView));
				}));
			}
		}


		public RelayCommand DeleteRecordCommand
		{
			get
			{
				return deleteRecordCommand ?? (deleteRecordCommand = new RelayCommand(() =>
				{
					Record record = (Record)selectedRecord;
					DbEntityEntry<Record> recordEntry = Store.Entry(record);
					if (recordEntry.State != EntityState.Detached)
					{
						Store.RecordSet.Local.Remove(record);
					}
					Store.SaveChanges();
					AssignNewRecord();
				}));
			}
		}

		public RelayCommand DiscardRecordCommand
		{
			get
			{
				return discardRecordCommand ?? (discardRecordCommand = new RelayCommand(() =>
				{
					DbEntityEntry<Record> recordEntry = Store.Entry((Record)selectedRecord);
					if (recordEntry.State == EntityState.Modified)
					{
						recordEntry.Reload();
					}
					Store.SaveChanges();
					AssignNewRecord();
				}));
			}
		}

		public RelayCommand EditCommand
		{
			get
			{
				return editCommand ?? (editCommand = new RelayCommand(() =>
				{
					ViewState.Push(new AccountEditViewModel(account, StoreView));
				}));
			}
		}

		private RelayCommand<RecordInfoViewModel> editRecordCommand;

		public RelayCommand<RecordInfoViewModel> EditRecordCommand
		{
			get
			{
				return editRecordCommand ?? (editRecordCommand = new RelayCommand<RecordInfoViewModel>(record =>
				{
					ViewState.Push(new RecordEditViewModel((Record)record, StoreView));
				}));
			}
		}

		public ReactiveProperty<string> Name { get; }

		public IReactiveDerivedList<RecordInfoViewModel> Records { get; }

		public RecordInfoViewModel SelectedRecord
		{
			get
			{
				return selectedRecord;
			}
			set
			{
				if (selectedRecord == value) return;
				selectedRecord = value;
				OnPropertyChanged(nameof(SelectedRecord));
			}
		}

		public AccountDetailViewModel(Account account, StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			this.account = account;
			Name = this.account.ToReactivePropertyAsSynchronized(a => a.Name);
			Records = this.account.Records.CreateDerivedCollection(r => new RecordInfoViewModel(r), null, (l, r) => DateTime.Compare(l.Timestamp.Value, r.Timestamp.Value));
			AssignNewRecord();
		}

		private void AssignNewRecord()
		{
			SelectedRecord = new RecordInfoViewModel(Store.RecordSet.Create());
			SelectedRecord.Timestamp.Value = DateTime.Today;
		}
	}
}
