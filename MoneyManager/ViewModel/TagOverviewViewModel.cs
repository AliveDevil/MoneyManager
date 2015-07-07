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

using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class TagOverviewViewModel : StoreViewModelBase
	{
		public IReactiveDerivedList<TagInfoViewModel> Tags { get; set; }

		private TagInfoViewModel selectedTag;

		public TagInfoViewModel SelectedTag
		{
			get { return selectedTag; }
			set
			{
				if (selectedTag == value) return;
				selectedTag = value;
				OnPropertyChanged(nameof(SelectedTag));
			}
		}

		private RelayCommand<TagInfoViewModel> deleteTagCommand;

		public RelayCommand<TagInfoViewModel> DeleteTagCommand
		{
			get
			{
				return deleteTagCommand ?? (deleteTagCommand = new RelayCommand<TagInfoViewModel>(tag =>
				{

				}));
			}
		}

		private RelayCommand addTagCommand;

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

		public TagOverviewViewModel(StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			Tags = viewModel.Store.TagSet.Local.CreateDerivedCollection(t => new TagInfoViewModel(t, StoreView));
		}
	}
}
