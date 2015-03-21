using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MoneyManager.ViewModel
{
	public class ViewModelLocator
	{
		public AccountListViewModel Accounts
		{
			get
			{
				return ServiceLocator.Current.GetInstance<AccountListViewModel>();
			}
		}

		public MainViewModel Main
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainViewModel>();
			}
		}

		public TagListViewModel Tags
		{
			get
			{
				return ServiceLocator.Current.GetInstance<TagListViewModel>();
			}
		}

		public RecordListViewModel Records
		{
			get
			{
				return ServiceLocator.Current.GetInstance<RecordListViewModel>();
			}
		}

		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<AccountListViewModel>();
			SimpleIoc.Default.Register<TagListViewModel>();
			SimpleIoc.Default.Register<RecordListViewModel>();
		}

		public static void Cleanup()
		{
		}
	}
}
