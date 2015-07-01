using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class TagInfoViewModel : StoreViewModelBase
	{
		private Tag tag;
		
		public ReactiveProperty<string> Key { get; set; }

		public TagInfoViewModel(Tag tag, StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			this.tag = tag;
			Key = this.tag.ToReactivePropertyAsSynchronized(t => t.Key);
		}
	}
}