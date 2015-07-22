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
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Win32;
using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class StoreLoadViewModel : ViewModelBase
	{
		public class MoneyData
		{
			public Decimal StartAmount { get; set; }
			public MoneyRecord[] Records { get; set; }
		}
		public class MoneyRecord
		{
			public Guid Id { get; set; }
			public DateTime Date { get; set; }
			public string Description { get; set; }
			public Decimal Amount { get; set; }
		}

		private RelayCommand aboutCommand;
		private RelayCommand backCommand;
		private RelayCommand donateCommand;
		private RelayCommand loadStoreCommand;
		private string name;
		private string path;
		private RelayCommand selectStoreLocationCommand;

		public RelayCommand AboutCommand
		{
			get
			{
				return aboutCommand ?? (aboutCommand = new RelayCommand(() =>
				{
					App.ViewState.Push<AboutViewModel>();
				}));
			}
		}

		public RelayCommand BackCommand
		{
			get
			{
				return backCommand ?? (backCommand = new RelayCommand(() =>
				{
					App.ViewState.Pop();
				}));
			}
		}

		public RelayCommand DonateCommand
		{
			get
			{
				return donateCommand ?? (donateCommand = new RelayCommand(() =>
				{
					Process.Start("http://donation-tracker.com/u/alivedevil");
				}));
			}
		}

		public RelayCommand LoadStoreCommand
		{
			get
			{
				return loadStoreCommand ?? (loadStoreCommand = new RelayCommand(() =>
				{
					SettingsDatabaseEntry databaseEntry = App.AppSettings.NewEntry(name, path);
					DatabaseContext context;
					if (System.IO.Path.GetExtension(databaseEntry.Path) == ".mmgr")
					{
						MoneyData data;
						using (FileStream fileStream = new FileStream(databaseEntry.Path, FileMode.Open))
						using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
						{
							XmlSerializer serializer = new XmlSerializer(typeof(MoneyData));
							data = (MoneyData)serializer.Deserialize(gzipStream);
						}
						databaseEntry.Path = System.IO.Path.ChangeExtension(databaseEntry.Path, ".store");
						context = new DatabaseContext(databaseEntry.Path, true);
						foreach (var item in data.Records)
						{
							Record record = context.RecordSet.Create();
							record.Account = context.AccountSet.First();
							record.Timestamp = item.Date;
							if (record.Timestamp < SqlDateTime.MinValue.Value)
							{
								record.Timestamp = SqlDateTime.MinValue.Value.Date;
							}
							record.Tag = context.TagSet.First();
							record.Description = item.Description;
							record.Value = (float)item.Amount;
							context.RecordSet.Add(record);
						}
					}
					else
					{
						context = new DatabaseContext(databaseEntry.Path, false);
					}
					StoreViewModel store = App.ViewState.Set<StoreViewModel>();
					store.Store = context;
				}));
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (name == value) return;
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Path
		{
			get
			{
				return path;
			}
			set
			{
				if (path == value) return;
				path = value;
				OnPropertyChanged(nameof(Path));
			}
		}

		public RelayCommand SelectStoreLocationCommand
		{
			get
			{
				return selectStoreLocationCommand ?? (selectStoreLocationCommand = new RelayCommand(() =>
				{
					OpenFileDialog dialog = new OpenFileDialog()
					{
						AddExtension = true,
						CheckPathExists = true,
						DereferenceLinks = true,
						FileName = "New Store.store",
						Filter = "MoneyManager Store|*.mmstore;*.store;*.sdf",
						InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
						RestoreDirectory = true,
						Title = "Select Store location"
					};
					if (dialog.ShowDialog() == true) Path = dialog.FileName;
				}));
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!disposedValue && disposing)
			{
				aboutCommand = null;
				backCommand = null;
				selectStoreLocationCommand = null;
				loadStoreCommand = null;
			}
			base.Dispose(disposing);
		}
	}
}
