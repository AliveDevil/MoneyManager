using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyManager.Model;

namespace MoneyManager.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		private List<MoneyRecordViewModel> collection;

		private MoneyRecordViewModel record;

		public MoneyRecordViewModel Record
		{
			get { return record; }
			set { record = value; }
		}


		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			collection = new List<MoneyRecordViewModel>();
			Record = new MoneyRecordViewModel(new MoneyRecord());
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
			}
			else
			{
				// Code runs "for real"
			}
		}

		RelayCommand applyCommand;
		public RelayCommand ApplyCommand
		{
			get
			{
				return applyCommand ?? (applyCommand = new RelayCommand(() =>
					{
						collection.Add(Record);
						Record.Reset();
					}));
			}
		}
	}
}