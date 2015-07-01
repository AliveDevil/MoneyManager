using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class TagOverviewViewModel : StoreViewModelBase
	{
		public IReactiveDerivedList<TagInfoViewModel> Tags { get; set; }

		public TagOverviewViewModel(StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			Tags = viewModel.Store.TagSet.Local.CreateDerivedCollection(t => new TagInfoViewModel(t, StoreView));
		}
	}
}
