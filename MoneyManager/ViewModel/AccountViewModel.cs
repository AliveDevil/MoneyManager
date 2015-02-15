using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using ReactiveUI;
using System;
using System.Data.Entity;
using System.Linq;

namespace MoneyManager.ViewModel
{
	public sealed class AccountViewModel : MyViewModelBase
	{
		private Account account;

		private RelayCommand applyCommand;
		private RecordViewModel record;
		private RelayCommand resetCommand;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					if (DatabaseContext.Instance.Entry((Record)Record).State == EntityState.Detached)
					{
						DatabaseContext.Instance.RecordSet.Add((Record)Record);
					}
					DatabaseContext.Instance.SaveChanges();
					NewRecord();
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
				RaiseAndSave("Name");
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

		public IReactiveDerivedList<RecordViewModel> Records { get; set; }

		public RelayCommand ResetCommand
		{
			get
			{
				return resetCommand ?? (resetCommand = new RelayCommand(async () =>
				{
					EntityState state = DatabaseContext.Instance.Entry((Record)Record).State;
					if (state == EntityState.Modified)
					{
						await DatabaseContext.Instance.Entry((Record)Record).ReloadAsync();
					}
					NewRecord();
				}));
			}
		}

		public AccountViewModel(Account account)
		{
			this.account = account;
			this.Records = this.account.Records.CreateDerivedCollection(
				record => new RecordViewModel(record),
				null,
				(l, r) => l.Timestamp > r.Timestamp ? 1 : r.Timestamp < l.Timestamp ? -1 : 0);
			NewRecord();
		}

		private void NewRecord()
		{
			RecordViewModel record = new RecordViewModel(DatabaseContext.Instance.RecordSet.Create());
			((Record)record).Timestamp = DateTime.Today;
			((Record)record).Tag = DatabaseContext.Instance.TagSet.First();
			((Record)record).Account = account;
			Record = record;
		}
	}
}
