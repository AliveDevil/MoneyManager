using MoneyManager.Model;
using System;
using System.IO;
using System.Windows;

namespace MoneyManager
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App : Application
	{
		private static Money database;
		private static string UserDocumentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MoneyManager");
		private static string UserDataPath = Path.Combine(UserDocumentPath, "data.xml");

		public static Money Database
		{
			get { return database; }
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			database = new Money();

			if (!Directory.Exists(UserDocumentPath))
			{
				Directory.CreateDirectory(UserDocumentPath);
			}

			database.ReadXml(UserDataPath);

			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			database.WriteXml(UserDataPath);

			base.OnExit(e);
		}
	}
}
