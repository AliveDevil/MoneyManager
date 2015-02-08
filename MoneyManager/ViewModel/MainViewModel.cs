using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using MoneyManager.Model;
using ReactiveUI;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace MoneyManager.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private AccountViewModel account;

		private RelayCommand addCommand;

		private RelayCommand deleteCommand;

		public AccountViewModel Account
		{
			get
			{
				return account;
			}
			set
			{
				account = value;
				RaisePropertyChanged("Account");
			}
		}

		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ?? (addCommand = new RelayCommand(() =>
				{
					var account = DatabaseContext.Instance.AccountSet.Create();
					account.Name = "Account";
					DatabaseContext.Instance.AccountSet.Add(account);
					DatabaseContext.Instance.SaveChanges();
				}));
			}
		}

		private RelayCommand changeLocation;

		public RelayCommand ChangeLocation
		{
			get
			{
				return changeLocation ?? (changeLocation = new RelayCommand(() =>
				{
					FolderBrowserDialog folderBrowser = new FolderBrowserDialog()._(_ =>
					{
						_.RootFolder = Environment.SpecialFolder.MyComputer;
						_.SelectedPath = (App.Current as App).InitialPath;
						_.ShowNewFolderButton = true;
					});

					if (folderBrowser.ShowDialog() == DialogResult.OK)
					{
						if (System.Windows.MessageBox.Show(Properties.Resources.EditNotNumeric, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
						{
							App.SetPath(folderBrowser.SelectedPath);
#if !DEBUG
							Process.Start(Application.ResourceAssembly.Location);
#endif
							System.Windows.Application.Current.Shutdown();
						}
					}
				}));
			}
		}


		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ?? (deleteCommand = new RelayCommand(() =>
				{
					if (System.Windows.MessageBox.Show("If you delete this account, every record is deleted aswell.", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
					{
						foreach (var item in Account.Records)
						{
						}
					}
				}));
			}
		}

		public IReactiveDerivedList<AccountViewModel> View { get; set; }

		public MainViewModel()
		{
			if (IsInDesignMode)
			{
			}
			else
			{
				DatabaseContext.Instance.AccountSet.Load();
				View = DatabaseContext.Instance.AccountSet.Local.CreateDerivedCollection(account => new AccountViewModel(account));
			}
		}
	}
}
