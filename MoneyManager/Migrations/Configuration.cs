using MoneyManager.Model;
using System.Data.Entity.Migrations;

namespace MoneyManager.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(DatabaseContext context)
		{
			context.TagSet.AddOrUpdate(p => p.Key, new Tag() { Key = "Default" });
		}
	}
}
