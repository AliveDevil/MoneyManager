using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class StoreViewModelBase : StateViewModelBase
	{
		protected StoreViewModel StoreView { get; }

		protected DatabaseContext Store => StoreView.Store;

		public StoreViewModelBase(StoreViewModel viewModel) : base(viewModel.ViewState)
		{
			StoreView = viewModel;
		}
	}
}
