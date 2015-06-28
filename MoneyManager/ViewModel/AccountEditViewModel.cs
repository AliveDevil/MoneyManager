using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class AccountEditViewModel : StateViewModelBase
	{
		private Account account;

		public ReactiveProperty<string> Name { get; }

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


		public AccountEditViewModel(Account account, ViewStateManager viewState) : base(viewState)
		{
			this.account = account;
			Name = this.account.ToReactivePropertyAsSynchronized(a => a.Name);
		}
	}
}