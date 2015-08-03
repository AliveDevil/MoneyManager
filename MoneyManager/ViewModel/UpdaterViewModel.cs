using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using nUpdate.UpdateEventArgs;

namespace MoneyManager.ViewModel
{
	public class UpdaterViewModel : ViewModelBase
	{
		private RelayCommand aboutCommand;
		private RelayCommand applyCommand;
		private RelayCommand backCommand;
		private RelayCommand donateCommand;
		private DownloadPackageReporter reporter = new DownloadPackageReporter();

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

		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(async () =>
				{
					await Task.Yield();
					if (App.UpdatesAvailable)
					{
						await App.Updater.DownloadPackagesAsync(Reporter);
						App.Updater.InstallPackage();
					}
				}));
			}
		}

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

		public DownloadPackageReporter Reporter
		{
			get
			{
				return reporter;
			}
			set
			{
				if (reporter == value) return;
				reporter = value;
				OnPropertyChanged(nameof(Reporter));
			}
		}

		public class DownloadPackageReporter : IProgress<UpdateDownloadProgressChangedEventArgs>, INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;

			private float progress;

			public float Progress
			{
				get
				{
					return progress;
				}
				set
				{
					if (progress == value) return;
					progress = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
				}
			}

			public void Report(UpdateDownloadProgressChangedEventArgs value)
			{
				Progress = value.Percentage;
			}
		}
	}
}
