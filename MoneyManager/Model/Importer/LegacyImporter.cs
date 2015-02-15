using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using MoneyManager.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace MoneyManager.Model.Importer
{
	public static class LegacyImporter
	{
		private static RelayCommand importCommand;

		public static RelayCommand ImportCommand
		{
			get
			{
				return importCommand ?? (importCommand = new RelayCommand(() =>
				{
					OpenFileDialog openFileDialog = new OpenFileDialog()._(@_ =>
					{
						@_.CheckFileExists = true;
						@_.CheckPathExists = true;
						@_.Filter = "MoneyManager Legacy File|*.mmgr";
						@_.InitialDirectory = Path.GetDirectoryName(Application.ResourceAssembly.Location);
						@_.Multiselect = false;
						@_.ValidateNames = true;
					});
					bool? result = openFileDialog.ShowDialog();
					if (result.HasValue && result.Value)
					{
						Account account = (Account)ServiceLocator.Current.GetInstance<AccountListViewModel>().Account;
					}
				}));
			}
		}
	}
}
