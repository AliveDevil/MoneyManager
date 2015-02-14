using MoneyManager.Model;
using System.Data.Entity.Migrations;
using System.Linq;
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
			if (!context.AccountSet.Any())
			{
				context.AccountSet.Add(new Account() { Name = "Default" });
			}
			context.TagSet.AddOrUpdate(p => p.Key, new Tag() { Key = "Default", Default = true });
		}
	}
}
