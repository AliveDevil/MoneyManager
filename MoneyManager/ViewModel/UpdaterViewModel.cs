using System.Diagnostics;

namespace MoneyManager.ViewModel
{
	public class UpdaterViewModel : ViewModelBase
	{
		private RelayCommand backCommand;
		private RelayCommand aboutCommand;
		private RelayCommand donateCommand;

		public RelayCommand BackCommand
		{
			get
			{
				return backCommand ?? (backCommand = new RelayCommand(() =>
				{
					App.ViewState.Pop();
				}));
			}
		}

		public RelayCommand AboutCommand
		{
			get
			{
				return aboutCommand ?? (aboutCommand = new RelayCommand(() =>
				{
					App.ViewState.Push<AboutViewModel>();
				}));
			}
		}

		public RelayCommand DonateCommand
		{
			get
			{
				return donateCommand ?? (donateCommand = new RelayCommand(() =>
				{
					Process.Start("http://donation-tracker.com/u/alivedevil");
				}));
			}
		}
	}
}
