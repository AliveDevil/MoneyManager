using System;
using System.Linq;
using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class RecordAddViewModel : StoreViewModelBase
	{
		private Account account;
		private RelayCommand applyCommand;
		private RelayCommand cancelCommand;
		private string description;
		private TagInfoViewModel tag;
		private DateTime timestamp;
		private float value;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					Record record = Store.RecordSet.Create();
					record.Account = account;
					record.Timestamp = Timestamp;
					record.Tag = (Tag)Tag;
					record.Description = Description;
					record.Value = Value;
					Store.RecordSet.Add(record);
					Store.SaveChanges();
					ViewState.Pop();
				}));
			}
		}

		public RelayCommand CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new RelayCommand(() =>
				{
					ViewState.Pop();
				}));
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				if (description == value) return;
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		public TagInfoViewModel Tag
		{
			get
			{
				return tag;
			}
			set
			{
				if (tag == value) return;
				tag = value;
				OnPropertyChanged(nameof(Tag));
			}
		}

		public IReactiveDerivedList<TagInfoViewModel> Tags { get; }

		public DateTime Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				if (timestamp == value) return;
				timestamp = value;
				OnPropertyChanged(nameof(Timestamp));
			}
		}

		public float Value
		{
			get
			{
				return value;
			}
			set
			{
				if (this.value == value) return;
				this.value = value;
				OnPropertyChanged(nameof(Value));
			}
		}

		public RecordAddViewModel(Account account, StoreViewModel viewModel) : base(viewModel)
		{
			this.account = account;
			description = "New Description";
			timestamp = DateTime.Today;
			value = 0;
			Tags = Store.TagSet.Local.CreateDerivedCollection(t => new TagInfoViewModel(t, StoreView));
			Tag = Tags.First(t => t.Default.Value);
		}
	}
}
