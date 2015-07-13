using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class RecordEditViewModel : StoreViewModelBase
	{
		private Record record;

		public ReactiveProperty<DateTime> Timestamp { get; set; }
		public ReactiveProperty<string> Description { get; set; }
		public ReactiveProperty<float> Value { get; set; }
		public ReactiveProperty<TagInfoViewModel> Tag { get; set; }
		public IReactiveDerivedList<TagInfoViewModel> Tags { get; set; }

		private RelayCommand applyCommand;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					Store.SaveChanges();
					ViewState.Pop();
				}));
			}
		}

		private RelayCommand cancelCommand;

		public RelayCommand CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new RelayCommand(() =>
				{
					DbEntityEntry<Record> entity = Store.Entry(record);
					if (entity.State == EntityState.Modified) entity.Reload();
					Store.SaveChanges();
					ViewState.Pop();
				}));
			}
		}



		public RecordEditViewModel(Record record, StoreViewModel viewModel) : base(viewModel)
		{
			this.record = record;
			Timestamp = this.record.ToReactivePropertyAsSynchronized(r => r.Timestamp);
			Description = this.record.ToReactivePropertyAsSynchronized(r => r.Description);
			Value = this.record.ToReactivePropertyAsSynchronized(r => r.Value);
			Tags = Store.TagSet.CreateDerivedCollection(t => new TagInfoViewModel(t, StoreView));
			Tag = this.record.ToReactivePropertyAsSynchronized(t => t.Tag, t => Tags.Single(tag => (Tag)tag == t), t => (Tag)t);
		}
	}
}
