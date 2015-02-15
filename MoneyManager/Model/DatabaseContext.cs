using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MoneyManager.Model
{
	public class Account
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ObservableCollection<Record> Records { get; set; }

		public Account()
		{
			Records = new ObservableCollection<Record>();
		}
	}

	public class DatabaseContext : DbContext
	{
		private static Lazy<DatabaseContext> instance = new Lazy<DatabaseContext>();

		public static DatabaseContext Instance
		{
			get { return instance.Value; }
		}

		public static bool IsCreated
		{
			get { return instance.IsValueCreated; }
		}

		public Tag DefaultTag { get { return TagSet.Where(tag => tag.Default).Single(); } }

		public virtual DbSet<Account> AccountSet { get; set; }

		public virtual DbSet<Record> RecordSet { get; set; }

		public virtual DbSet<Tag> TagSet { get; set; }

		public DatabaseContext()
			: base("name=Money")
		{
			Database.Initialize(false);
			AccountSet.Load();
			TagSet.Load();
			RecordSet.Load();
		}
	}

	public class Record
	{
		public virtual Account Account { get; set; }

		public string Description { get; set; }

		public int Id { get; set; }

		public Tag Tag { get; set; }

		public DateTime Timestamp { get; set; }

		public float Value { get; set; }
	}

	public class Tag
	{
		public int Id { get; set; }

		[Index(IsUnique = true)]
		public string Key { get; set; }

		public bool Default { get; set; }

		public virtual ObservableCollection<Record> Records { get; set; }

		public Tag()
		{
			Records = new ObservableCollection<Record>();
		}
	}
}
