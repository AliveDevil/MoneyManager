/*
Copyright (C) 2015  Jöran Malek

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using MoneyManager.Model;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class TagOverviewViewModel : StoreViewModelBase
	{
		private RelayCommand addTagCommand;
		private RelayCommand<TagInfoViewModel> deleteTagCommand;
		private RelayCommand<TagInfoViewModel> editTagCommand;

		public RelayCommand AddTagCommand
		{
			get
			{
				return addTagCommand ?? (addTagCommand = new RelayCommand(() =>
				{
					StoreView.ViewState.Push(new TagAddViewModel(StoreView));
				}));
			}
		}

		public RelayCommand<TagInfoViewModel> DeleteTagCommand
		{
			get
			{
				return deleteTagCommand ?? (deleteTagCommand = new RelayCommand<TagInfoViewModel>(tag =>
				{
				}));
			}
		}

		public RelayCommand<TagInfoViewModel> EditTagCommand
		{
			get
			{
				return editTagCommand ?? (editTagCommand = new RelayCommand<TagInfoViewModel>(tag =>
				{
					StoreView.ViewState.Push(new TagEditViewModel((Tag)tag, StoreView));
				}));
			}
		}

		public IReactiveDerivedList<TagInfoViewModel> Tags { get; set; }

		public TagOverviewViewModel(StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			Tags = viewModel.Store.TagSet.Local.CreateDerivedCollection(t => new TagInfoViewModel(t, StoreView));
		}
	}
}
