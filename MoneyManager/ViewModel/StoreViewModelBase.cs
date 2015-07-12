using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class StoreViewModelBase : StateViewModelBase
	{
		protected DatabaseContext Store => StoreView.Store;

		protected StoreViewModel StoreView { get; }

		public StoreViewModelBase(StoreViewModel viewModel) : base(viewModel.ViewState)
		{
			StoreView = viewModel;
		}
	}
}
