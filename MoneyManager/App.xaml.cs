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
using MoneyManager.ViewModel;

namespace MoneyManager
{
	public partial class App : Application
	{
		public ViewStateManager ViewState { get; private set; }
		public Settings AppSettings { get; private set; }
		private FileInfo settingsFile;

		public void SaveSettings()
		{
			Settings.Save(AppSettings, settingsFile);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			Settings.Save(AppSettings, settingsFile);
			ViewState.Clear();
			base.OnExit(e);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());

			ViewState = new ViewStateManager();
			DirectoryInfo directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MoneyManager"));
			if (!directory.Exists) directory.Create();
			settingsFile = new FileInfo(Path.Combine(directory.FullName, "settings.json"));
			if (settingsFile.Exists)
			{
				AppSettings = Settings.Load(settingsFile);
			}
			else
			{
				AppSettings = new Settings();
				Settings.Save(AppSettings, settingsFile);
			}

			ViewState.Set<LoadDatabaseViewModel>();

			base.OnStartup(e);
		}
	}
}
