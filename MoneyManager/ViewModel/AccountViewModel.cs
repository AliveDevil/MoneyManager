using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using ReactiveUI;
using System;
using System.Data.Entity;
using System.Linq;

namespace MoneyManager.ViewModel
{
	public sealed class AccountViewModel : ViewModelBase
	{
		private Account account;

		private RelayCommand applyCommand;
		private RecordViewModel record;
		private IReactiveDerivedList<RecordViewModel> records;

		private RelayCommand resetCommand;
		private IReactiveDerivedList<TagViewModel> tags;

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
			get
			{
				return records ?? (records = this.account.Records.CreateDerivedCollection(
				record => new RecordViewModel(record),
				null,
				(l, r) => l.Timestamp > r.Timestamp ? 1 : r.Timestamp < l.Timestamp ? -1 : 0));
			}
		}

		public RelayCommand ResetCommand
		{
			get
			{
				return resetCommand ?? (resetCommand = new RelayCommand(async () =>
				{
					await DatabaseContext.Instance.Entry((Record)Record).ReloadAsync();
					NewRecord();
				}));
			}
		}

		public IReactiveDerivedList<TagViewModel> Tags
		{
			get { return tags ?? (tags = DatabaseContext.Instance.TagSet.CreateDerivedCollection(tag => new TagViewModel(tag))); }
		}

		public AccountViewModel(Account account)
		{
			this.account = account;
			NewRecord();
		}

		private void NewRecord()
		{
			Record = new RecordViewModel(DatabaseContext.Instance.RecordSet.Create());
			((Record)Record).Timestamp = DateTime.Today;
			((Record)Record).Tag = DatabaseContext.Instance.TagSet.First();
			((Record)Record).Account = account;
		}
	}
}
