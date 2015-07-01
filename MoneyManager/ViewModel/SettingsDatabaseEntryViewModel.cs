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

namespace MoneyManager.ViewModel
{
	public class SettingsDatabaseEntryViewModel : ViewModelBase
	{
		private SettingsDatabaseEntry entry;

		public string Name
		{
			get
			{
				return entry?.Name ?? "None";
			}
			set
			{
				if (entry == null) return;
				if (entry.Name == value) return;
				entry.Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Path
		{
			get
			{
				return entry?.Path ?? string.Empty;
			}
			set
			{
				if (entry == null) return;
				if (entry.Path == value) return;
				entry.Path = value;
				OnPropertyChanged(nameof(Path));
			}
		}

		public SettingsDatabaseEntryViewModel()
		{
		}

		public SettingsDatabaseEntryViewModel(SettingsDatabaseEntry settingsDatabaseEntry)
		{
			if (InDesignMode) return;
			entry = settingsDatabaseEntry;
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue && disposing)
			{
				entry = null;
			}
			base.Dispose(disposing);
		}
	}
}
