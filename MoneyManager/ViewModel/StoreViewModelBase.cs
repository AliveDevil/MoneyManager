using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
    public class StoreViewModelBase : ViewModelBase
    {
        protected DatabaseContext Store => StoreView.Store;

        protected StoreViewModel StoreView { get; }

        public StoreViewModelBase(StoreViewModel viewModel)
        {
            StoreView = viewModel;
        }
    }
}
