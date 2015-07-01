using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class AccountEditViewModel : StoreViewModelBase
	{
		private Account account;

		private RelayCommand finishCommand;

		public RelayCommand FinishCommand
		{
			get
			{
				return finishCommand ?? (finishCommand = new RelayCommand(() =>
				{
					ViewState.Pop();
				}));
			}
		}

		public ReactiveProperty<string> Name { get; }

		public AccountEditViewModel(Account account, StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			this.account = account;
			Name = this.account.ToReactivePropertyAsSynchronized(a => a.Name);
		}
	}
}
