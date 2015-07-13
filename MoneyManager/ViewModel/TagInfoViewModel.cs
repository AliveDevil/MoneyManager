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
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class TagInfoViewModel : StoreViewModelBase
	{
		private Tag tag;

		public ReactiveProperty<string> Key { get; set; }

		public ReactiveProperty<bool> Default { get; set; }

		public TagInfoViewModel(Tag tag, StoreViewModel viewModel) : base(viewModel)
		{
			if (InDesignMode) return;
			this.tag = tag;
			Key = this.tag.ToReactivePropertyAsSynchronized(t => t.Key);
			Default = this.tag.ToReactivePropertyAsSynchronized(t => t.Default);
		}

		public static explicit operator Tag(TagInfoViewModel tagInfo)
		{
			return tagInfo.tag;
		}
	}
}
