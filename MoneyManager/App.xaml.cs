using FUpdate;
using MoneyManager.Migrations;
using MoneyManager.Model;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyManager
{
	public partial class App : Application
	{
		private static readonly System.Configuration.Configuration Configuration;
		private static readonly KeyValueConfigurationElement PathElement;

		private string initialPath;
		private static bool restart = false;

		public string InitialPath { get { return initialPath; } }

		static App()
		{
			Configuration = ConfigurationManager.OpenExeConfiguration(Application.ResourceAssembly.Location);
			if (!Configuration.AppSettings.Settings.AllKeys.Contains("Path"))
			{
				Configuration.AppSettings.Settings.Add("Path", "");
			}
			PathElement = Configuration.AppSettings.Settings["Path"];
			if (string.IsNullOrEmpty(PathElement.Value))
			{
#if DEBUG
				SetPath(Path.GetFullPath("MoneyManager"));
#else
				SetPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MoneyManager"));
#endif
			}
		}

		public static void SetPath(string path)
		{
			PathElement.Value = path;
			Configuration.Save();
			restart = true;
		}

		protected override void OnExit(ExitEventArgs e)
		{
			if (DatabaseContext.IsCreated)
			{
				DatabaseContext.Instance.SaveChanges();
			}

			if (!initialPath.Equals(PathElement.Value))
			{
				File.Move(Path.Combine(initialPath, "MoneyManager.sdf"), Path.Combine(PathElement.Value, "MoneyManager.sdf"));
				if (restart)
				{
					Process.Start(ResourceAssembly.Location);
				}
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

#if !DEBUG
			Task.Factory.StartNew(SearchUpdate);
#endif

			base.OnStartup(e);
		}

#if !DEBUG
		private void SearchUpdate()
		{
			Update update = new Update(
				@"<RSAKeyValue><Modulus>lY/rdgOvgn+IJwuobE0LvCTg3dzL3dLHCuoTY7oxi6ny7XqPMOWW83Vda/rA15LKJGL3Sf3TSFM5x+y4X/97To5rmY8DKsanhc/ta36jhbgURfjHdyJtz0wFvsS5d9RbkFujN6NTZf/KYTIOg30XfJwwq1MOZZrRbOQoG7RnOemaBV9ccdXgVrArn+SMB76McBcDhrtha4gixSz7+EnBkErBek5sWindoUurQU+pzxhRlx/qM70KuwtO9jEuQ1AE0VC8MpJTMLSbCJNj3JtQGS6Gff5ezJF979lG1TWVBKkWXNpp4QOBMs49nQwEAiN/QWtn85L7z399kisMh3m1CjuUuJ8KcU1hd1yYAui2Xa4def4LUVWePu++71vvwz58fVVQ5WPb63d3a5/z8FRC7anSSQMkEdyKrEiumvswqluXoNB4ujbEvCm9e2jrSEuu58/To4+8YZq0DXwQBQw0BqhjR3J8bsVerX2aAlhG+z+ht3qdUPDgSMaMrej/9F9sl4dIz5sZPrKvKGOPc+rnChcQyZrO/UJ2Jl7m/MFzjV5naAtVkIKBt9m7s6Z1qFm6HK4KtS2OyJysHFbnorcsbcGYQnAi/CfKG8iWxBn3kcmCfYe+WeG1iqvvZ3sU8EXvolue1Rs+kdkwPOA8VleD42IbtMzSA8I1FsXcFZp1mlc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
				new Uri("http://alivedevil.de/update/MoneyManager/update.info"),
				true);
			if (update.Check())
			{
				update.Apply(false, true);
				Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
			}
		}
#endif
	}
}
