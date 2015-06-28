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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactiveUI;

namespace MoneyManager.ViewModel
{
	public class AccountDetailViewModel : StateViewModelBase
	{
		private Account account;

		public ReactiveProperty<string> Name { get; }

		public IReactiveDerivedList<AccountRecordViewModel> Records { get; }

		private RelayCommand editCommand;

		public RelayCommand EditCommand
		{
			get
			{
				return editCommand ?? (editCommand = new RelayCommand(() =>
				{
					ViewState.Push(new AccountEditViewModel(account, ViewState));
				}));
			}
		}

		public AccountDetailViewModel(Account account, ViewStateManager viewState) : base(viewState)
		{
			this.account = account;
			Name = this.account.ToReactivePropertyAsSynchronized(a => a.Name);
			Records = account.Records.CreateDerivedCollection(r => new AccountRecordViewModel(r));
		}
	}
}
