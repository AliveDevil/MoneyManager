using MoneyManager.Migrations;
using MoneyManager.Model;
using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Linq;

namespace MoneyManager
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App : Application
	{
		private static readonly System.Configuration.Configuration Configuration;
		private static readonly KeyValueConfigurationElement PathElement;

		private string initialPath;

		static App()
		{
			Configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			if (!Configuration.AppSettings.Settings.AllKeys.Contains("Path"))
			{
				Configuration.AppSettings.Settings.Add("Path", "");
			}
			PathElement = Configuration.AppSettings.Settings["Path"];
			if (string.IsNullOrEmpty(PathElement.Value))
			{
				SetPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MoneyManager"));
			}
		}

		public static void SetPath(string path)
		{
			PathElement.Value = path;
			Configuration.Save();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			if (DatabaseContext.IsCreated)
			{
				DatabaseContext.Instance.SaveChanges();
			}

			if (initialPath.Equals(PathElement.Value))
			{
				File.Move(Path.Combine(initialPath, "MoneyManager.sdf"), Path.Combine(PathElement.Value, "MoneyManager.sdf"));
			}

			base.OnExit(e);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			string path = PathElement.Value;
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			initialPath = path;
			AppDomain.CurrentDomain.SetData("DataDirectory", path);
			Database.SetInitializer<DatabaseContext>(new MigrateDatabaseToLatestVersion<DatabaseContext, MoneyManager.Migrations.Configuration>());

			base.OnStartup(e);
		}
	}
}
