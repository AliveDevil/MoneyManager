namespace MoneyManager.ViewModel
{
	public class StateViewModelBase : ViewModelBase
	{
		protected ViewStateManager ViewState { get; }

		public StateViewModelBase(ViewStateManager viewState)
		{
			if (InDesignMode) return;
			ViewState = viewState;
		}
	}
}
