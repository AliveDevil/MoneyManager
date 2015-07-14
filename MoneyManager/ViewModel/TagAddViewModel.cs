using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class TagAddViewModel : StoreViewModelBase
	{
		private RelayCommand applyCommand;
		private RelayCommand cancelCommand;
		private string name;

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
				{
					Tag tag = Store.TagSet.Create();
					tag.Key = Name;
					Store.TagSet.Add(tag);
					Store.SaveChanges();
					StoreView.ViewState.Pop();
				}));
			}
		}

		public RelayCommand CancelCommand
		{
			get
			{
				return cancelCommand ?? (cancelCommand = new RelayCommand(() =>
				{
					StoreView.ViewState.Pop();
				}));
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (name == value) return;
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public TagAddViewModel(StoreViewModel viewModel) : base(viewModel)
		{
			Name = "New Tag";
		}
	}
}
