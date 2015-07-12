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

using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MoneyManager.Model;
using MoneyManager.ViewModel;
using nUpdate.Updating;

namespace MoneyManager
{
	public partial class App : Application, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private FileInfo settingsFile;
		private UpdateManager updater;
		private bool updatesAvailable;

		public Settings AppSettings { get; private set; }

		public bool UpdatesAvailable
		{
			get
			{
				return updatesAvailable;
			}
			set
			{
				if (updatesAvailable == value) return;
				updatesAvailable = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UpdatesAvailable)));
			}
		}

		public ViewStateManager ViewState { get; private set; }

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

			updater = new UpdateManager(new Uri("http://dl.alivedevil.de/update/moneymanager/updates.json"), "<RSAKeyValue><Modulus>3a88jyS9mfHONnEOu8KVtOCcIUYC8rJGT88HIhr/tBWHc0V3oeuKPqnYA7RB097HlcrywKt8mm4/LTD3endINhlC5hhpqAMgGeajImmm7OKTfHNnOVd6iQKpEBq1yr9CK5XnBrviyW3kZiSVIIid0+U0ubSXvEIaNfg6LouUI/ndNtNwHfP0zAKyr5m7IXIJYitnhtH4c63fjVLFsdO8K5WiFRoJ29npfCrpc2rdm1uwP/xnfmT5O4GwYQeJm32PZ6zX78GIQ6MaadS88lwai1L+NuFapAx7aHy4P+2CsLeysBmmpAxbyNxmzqXG/m4hF8tN7lwdcmY9zI6HPmIhOL8H3ISTbHOrlIZIM58alo704WT68p9vWzTKKscHFHj7IeJHR9VEVauZ2BGZxpnwxVbmkVwT9aHt+v1ZxvuHtReW0sgP815dN8Dwb5jZO7esYirkt89k9jOAjh+ZRjnjuVoEpCKXChKKLhHjZmAD6e9kz5E0igwzJ/HQjPqXNhFf9H5NfgJY95FFL3cBjH9ON27auC2l4Pb5mhRl2IhnAciRWcOhICkDq44O/6G+01PWQC9fazRFCwNvz6PSm2zLKLPoAyPmH9vz1Q+pCeIYk2nRvRDIe8pUMMSeheHZuK4g1QIIQmuD47VZdZNBVPDafe7jQ4XHpnk4gNiLYKonRYvgYAIWMnFeJwSpX1uPf1zxzRNhFsx6FUFC2AWw/3mcJJUlp0NYGaIolgj9ednDen7ol8oeaFi8uajzXFBcPBlTekM89ACoMqzv7G915DWzimL8V7keIab75Mwc1MvqP0lgXD73YbwtXLykq2uZGpyk8L/10xdwk0RHftxn8g/k5GAO7aEJvO6MSDvKZ0GUeUyIN6nT4uOuFegIUPE+XLKGoUOkE3t3EWQKs7IlVUByiURuqz+llZh2bkQy/XHaybdJ6HiArckXGAJxDIlu27xld/luGHWduIdyPPvIx+QgBt93v2N1ASkPSlNrmtV7HO/1OvQX4dOFiPMUDJgOiecFUMlSeaejOoeJcsKrZV9zuz6K1PBXNDLL+YbF+0ER57SS8fT1w2uHwx1bVC/9pF6B4EXNcv7zNNNAs0x0iGsSoPTV4lUK+E0xdujxEKjYBpPiLM3ISyYfheD0A3tC4yZEjKaXNfk4rb59IFg2hDYoURsoYJnvJgsEu6kRI7SDlVW+Lmh++BICprLf3K3OdXrmEavjqvcp3xrSbDgKj3ubzHdWLS2JvuCcQMdvFlmqQn0yW+skq/AfgefviVQ7Wy0tLrN8jNCEGlOh0ujgxliWmex5zVschjvfNbfp5+lOxi4SbnUV9iNG5wi7qtxbTdIXBiknBdMnZ50KSf/4Ymc9UQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", new CultureInfo("en"));
			updater.CloseHostApplication = true;
			updater.UseCustomInstallerUserInterface = true;
			updater.IncludeBeta = AppSettings.CheckBetaVersions;

			CheckForUpdates();

			ViewState.Set<LoadDatabaseViewModel>();

			base.OnStartup(e);
		}

		private async Task CheckForUpdates()
		{
			if (await updater.SearchForUpdatesAsync())
			{
				updatesAvailable = true;
			}
		}
	}
}
