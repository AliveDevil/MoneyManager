using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		public CollectionViewSource View { get; set; }

		private ObservableCollection<MoneyRecordViewModel> collection;
		private MoneyRecordViewModel record;

		public MoneyRecordViewModel Record
		{
			get { return record; }
			set
			{
				record = value;
				RaisePropertyChanged("Record");
			}
		}

		public MainViewModel()
		{
			collection = new ObservableCollection<MoneyRecordViewModel>();
			Record = new MoneyRecordViewModel();

			if (IsInDesignMode)
			{
				collection.Add(new MoneyRecordViewModel(new MoneyRecord() { Date = DateTime.Now.Date, Description = "Test", Amount = 500 }));
			}
			else
			{
				
			}

			View.Source = collection;
		}

		RelayCommand applyCommand;
		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
					{
						collection.Add(Record);
						Record = new MoneyRecordViewModel();
					}));
			}
		}
	}
}