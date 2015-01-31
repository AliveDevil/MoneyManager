using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using ReactiveUI;
using System.Data.Entity;

namespace MoneyManager.ViewModel
{
	public sealed class AccountViewModel : ViewModelBase
	{
		private Account account;

		private RelayCommand applyCommand;
		private RecordViewModel record;
		private IReactiveDerivedList<RecordViewModel> records;

		private RelayCommand resetCommand;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					if (DatabaseContext.Instance.Entry((Record)record).State == EntityState.Detached)
					{
						DatabaseContext.Instance.RecordSet.Add((Record)record);
					}
					DatabaseContext.Instance.SaveChanges();
				}));
			}
		}

		public int Id
		{
			get
			{
				return account.Id;
			}
		}

		public string Name
		{
			get
			{
				return account.Name;
			}
			set
			{
				account.Name = value;
				RaisePropertyChanged("Name");
			}
		}

		public RecordViewModel Record
		{
			get
			{
				return record;
			}
			set
			{
				record = value;
				RaisePropertyChanged("Record");
			}
		}

		public IReactiveDerivedList<RecordViewModel> Records
		{
			get { return records ?? (records = this.account.Records.CreateDerivedCollection(record => new RecordViewModel(record))); }
		}

		public RelayCommand ResetCommand
		{
			get
			{
				return resetCommand ?? (resetCommand = new RelayCommand(async () =>
				{
					await DatabaseContext.Instance.Entry(record).ReloadAsync();
					NewRecord();
				}));
			}
		}

		public AccountViewModel(Account account)
		{
			this.account = account;
			NewRecord();
		}

		private void NewRecord()
		{
			Record = new RecordViewModel(DatabaseContext.Instance.RecordSet.Create());
			Record.Timestamp = DateTime.Today;
			((Record)record).Account = account;
		}
	}
}
