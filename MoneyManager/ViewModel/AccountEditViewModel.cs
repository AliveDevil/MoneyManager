using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class AccountEditViewModel : StateViewModelBase
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

		public AccountEditViewModel(Account account, ViewStateManager viewState) : base(viewState)
		{
			if (!InDesignMode)
			{
				this.account = account;
				Name = this.account.ToReactivePropertyAsSynchronized(a => a.Name);
			}
		}
	}
}
