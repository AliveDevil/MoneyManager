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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using ReactiveUI;

namespace MoneyManager.Model
{
	public class Account : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int id;
		private string name;

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
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
				name = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
			}
		}

		public virtual ObservableCollection<Record> Records { get; set; }

		public Account()
		{
			Records = new ObservableCollection<Record>();
		}
	}

	public class DatabaseContext : DbContext
	{
		public virtual DbSet<Account> AccountSet { get; set; }

		public Tag DefaultTag
		{
			get
			{
				return TagSet.Where(tag => tag.Default).Single();
			}
		}

		public virtual DbSet<Record> RecordSet { get; set; }

		public virtual DbSet<Tag> TagSet { get; set; }

		public DatabaseContext() : base() { }

		public DatabaseContext(string filename, bool createDatabase)
			: base($"Data Source={filename}")
		{
			if (!(Database.Exists() || createDatabase)) throw new FileNotFoundException();
			if (Database.Exists() && createDatabase) File.Delete(filename);
			if (!Database.Exists())
			{
				Database.Initialize(true);
				Database.Create();
				AccountSet.Add(new Account() { Name = "Default" });
				TagSet.Add(new Tag() { Default = true, Key = "Default" });
				SaveChanges();
			}
			else
			{
				Database.Initialize(true);
				AccountSet.Load();
				RecordSet.Load();
				TagSet.Load();
			}
		}
	}

	public class Record : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Account account;
		private string description;
		private int id;
		private Tag tag;
		private DateTime timestamp;
		private float value;

		public virtual Account Account
		{
			get
			{
				return account;
			}
			set
			{
				account = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Account)));
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
			}
		}

		public Tag Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tag)));
			}
		}

		public DateTime Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				timestamp = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Timestamp)));
			}
		}

		public float Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
			}
		}
	}

	public class Tag : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool @default;
		private int id;
		private string key;

		public bool Default
		{
			get
			{
				return @default;
			}
			set
			{
				@default = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Default)));
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
			}
		}

		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
			}
		}

		public virtual ReactiveList<Record> Records { get; set; }

		public Tag()
		{
			Records = new ReactiveList<Record>() { ChangeTrackingEnabled = true };
		}
	}
}
