using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MoneyManager.Model;
using ReactiveUI;
using System.Data.Entity;
using System.Windows;

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

		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ?? (deleteCommand = new RelayCommand(() =>
				{
					if (MessageBox.Show("If you delete this account, every record is deleted aswell.", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
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
