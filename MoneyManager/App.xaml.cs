using MoneyManager.Migrations;
using MoneyManager.Model;
using nUpdate.Internal;
using System;
using System.Configuration;
using System.Data.Entity;
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

			Task.Factory.StartNew(SearchUpdate);

			base.OnStartup(e);
		}

		private void SearchUpdate()
		{
			using (UpdateManager updateManager = new UpdateManager(
				new Uri("http://alivedevil.de/update/MoneyManager/updates.json"),
				"<RSAKeyValue><Modulus>uhFZnzwVsqKeawvW6Zssoo/wmc72tzbW9IQyjoFy9NWkJGlA73XM//IqGwRdbYigI9j/WoGrFEy7F7N/usyp+eAGzOTHen6BnaPrIzy2x8KKNPHxD5U+LBiWh8Pd9nl97o1Pqcu/wYyttX64qs7zKCuL5rj6+MeB1mIQbiE+YmzWJpciy2Io6r06PJb8E/1uVBynDdPKoYyZvDw4eG0oGBzF7flA1/WHd1Fj6sJGH5+XmO48hj2kCCb99kh06sSNUvIvREPEZB1MWtS609n9+ie4uizdi/9mF+3M8InQVBZOPp8XzDN2eqVgLZcZ2N33qv0YegPUCW37ewRMyo826ALupZJZdZdfrAZG2ejsXKCwlrmz6msRcakl+5wXKr/SuN1g3zRYVUvt/t43y/gwkzx6MiMy+YQEv7jHIVWxrxalLDTRV8ghP0eb5+4H/Dv0E3egYwn4SZJ1nVAHuS1R6cw3cMsy6Fdbc6MOkhpzJzYZIiaeIRqdJfJ3mpthBPUxh4L423ROFHM1vuhOed8IOCiLybjD2fVw/mvdTm5di5pIYafxhIk4akoXgRDuatP2KQ0pkFZRz5DSMyvxuJaCTz+t/7B8GrkDiE/0zr5aRAoWvDuxTncF8bFrYhVmsHoityMxrAbNjhQH32jwrsfrBwNiq7ghbnqfNPGxUsPC836frUsdiKWVdcBa5yDx3upiD0I15HcI8aT8cZ+m7cVjwn80b58LuVhI6NbvqVqwo8Oi4lkj6ixTaGVqdMSkdlRRH6LCq8ZWrTB78N6LfnCq3umfcLiV+k0nB5Tu/5pNRLilHIXFixrgQRtOQ+i5vy3WxGGybPfaBeHkV3uBOhGmQXvKnasNu6iuwcxME5w+qDqNszwhraf3AmO9r3AgATvXJxufcvgoOBjQPDOpLmgY70LqzLK0LThLd+sBxHK7joVIxZDSXnwNihD5e/MqxtytvP8amyBhM3sNzVjkJ4pRe27HWj6e/PUbfTQMsmudx3ljuthhkSW2JfVupOd3runGNaIEhCI363DrQ4JFUme0ynzYa5f0soRGDJlZgRsXg8jygubcn5zJdk0p8Ob1/PpMcMB+jMhi9igkmsXOaxMvGWhO04pU4NNZhXqqNdfaAgas9up5euLjgifuGeXLwvhB9708KFl1q2SfXMYZkLppnI4ulGeWoXSPyVP7xpYP2Qk9CspoqxDuAPgkYIsF0Cg7a9aO3ni7+sgwBGK2pSvz5MOn9jJGvyxb4LVLt5g2trlll7nKTncNDhVwyPqmouAZ0PlJxFtxrFxaiWbHUH5brFwSO1c064GdLRJPvE0iH0tnvg2t3tbfUcUdJPv8F55xcbxjYy2w9hbI4xQxamN3qQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
				new UpdateVersion("0.0.0.1a1"),
				new CultureInfo("en")))
			{
				updateManager.SearchForUpdates();
				if (updateManager.UpdatesFound)
				{
					updateManager.DownloadPackage();
					updateManager.CheckPackageValidity();
					updateManager.InstallPackage();
				}
			}
		}
	}
}
