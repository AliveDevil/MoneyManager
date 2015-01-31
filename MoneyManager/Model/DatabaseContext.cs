using System;
using System.Collections.ObjectModel;
using System.Data.Entity;

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

		public virtual DbSet<Account> AccountSet { get; set; }

		public virtual DbSet<Record> RecordSet { get; set; }

		public DatabaseContext()
			: base("name=Money")
		{
			Database.Initialize(false);
		}
	}

	public class Record
	{
		public virtual Account Account { get; set; }

		public string Description { get; set; }

		public int Id { get; set; }

		public DateTime Timestamp { get; set; }

		public float Value { get; set; }
	}
}
