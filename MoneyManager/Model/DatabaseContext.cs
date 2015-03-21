using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
			get
			{
				return instance.Value;
			}
		}

		public static bool IsCreated
		{
			get { return instance.IsValueCreated; }
		}

		public virtual DbSet<Account> AccountSet { get; set; }

		public Tag DefaultTag { get { return TagSet.Where(tag => tag.Default).Single(); } }

		public virtual DbSet<Record> RecordSet { get; set; }

		public virtual DbSet<Tag> TagSet { get; set; }

		public DatabaseContext()
			: base("name=Money")
		{
			Load();
		}

		private async Task Load()
		{
			await AccountSet.LoadAsync();
			await RecordSet.LoadAsync();
			await TagSet.LoadAsync();
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
		public bool Default { get; set; }

		public int Id { get; set; }

		[Index(IsUnique = true)]
		public string Key { get; set; }

		public virtual ObservableCollection<Record> Records { get; set; }

		public Tag()
		{
			Records = new ObservableCollection<Record>();
		}
	}
}
