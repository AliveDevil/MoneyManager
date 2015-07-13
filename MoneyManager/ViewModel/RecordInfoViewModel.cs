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
using MoneyManager.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MoneyManager.ViewModel
{
	public class RecordInfoViewModel : ViewModelBase
	{
		private Record record;

		public ReactiveProperty<string> Description { get; }

		public ReactiveProperty<DateTime> Timestamp { get; }

		public ReactiveProperty<float> Value { get; }

		public RecordInfoViewModel(Record record)
		{
			if (InDesignMode) return;
			this.record = record;
			Description = this.record.ToReactivePropertyAsSynchronized(r => r.Description);
			Timestamp = this.record.ToReactivePropertyAsSynchronized(r => r.Timestamp);
			Value = this.record.ToReactivePropertyAsSynchronized(r => r.Value);
		}

		public static explicit operator Record(RecordInfoViewModel recordView)
		{
			return recordView.record;
		}
	}
}
