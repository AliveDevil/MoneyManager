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
					Account account = Account;
					if (account == null)
					{
						MessageBox.Show("No account selected", "", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					OpenFileDialog openFileDialog = new OpenFileDialog()._(_ =>
					{
						_.CheckFileExists = true;
						_.CheckPathExists = true;
						_.Filter = "MoneyManager Legacy File|*.mmgr";
						_.InitialDirectory = Path.GetDirectoryName(Application.ResourceAssembly.Location);
						_.Multiselect = false;
						_.ValidateNames = true;
					});
					bool? result = openFileDialog.ShowDialog();
					if (result.HasValue && result.Value)
					{
						
					}
				}));
			}
		}

		private static Account Account
		{
			get
			{
				var listModel = ServiceLocator.Current.GetInstance<AccountListViewModel>();
				if (listModel == null)
					return null;
				var viewModel = listModel.Account;
				if (viewModel == null)
					return null;
				return (Account)viewModel;
			}
		}
	}
}
