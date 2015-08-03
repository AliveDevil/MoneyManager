using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class TagEditViewModel : StoreViewModelBase
	{
		private RelayCommand applyCommand;
		private RelayCommand cancelCommand;
		private Tag tag;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					Store.SaveChanges();
                    ViewStack.Pop();
				}));
			}
		}

		public RelayCommand CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new RelayCommand(() =>
				{
					DbEntityEntry<Tag> entry = Store.Entry(tag);
					if (entry.State == EntityState.Modified) entry.Reload();
					Store.SaveChanges();
                    ViewStack.Pop();
				}));
			}
		}

		public ReactiveProperty<string> Name { get; set; }

		public TagEditViewModel(Tag tag, StoreViewModel viewModel) : base(viewModel)
		{
			this.tag = tag;
			Name = tag.ToReactivePropertyAsSynchronized(t => t.Key);
		}
	}
}
