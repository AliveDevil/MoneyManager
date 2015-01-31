using GalaSoft.MvvmLight;
using MoneyManager.Model;
using System;
using System.Diagnostics;

namespace MoneyManager.ViewModel
{
	public class RecordViewModel : ViewModelBase
	{
		private Record record;

		public string Description
		{
			get
			{
				return record.Description;
			}
			set
			{
				record.Description = value;
				RaisePropertyChanged("Description");
			}
		}

		public DateTime Timestamp
		{
			get
			{
				return record.Timestamp;
			}
			set
			{
				record.Timestamp = value;
				RaisePropertyChanged("Timestamp");
			}
		}

		[DebuggerNonUserCode]
		public string Value
		{
			get
			{
				return record.Value.ToString();
			}
			set
			{
				record.Value = float.Parse(value);
				RaisePropertyChanged("Value");
			}
		}

		public RecordViewModel(Record record)
		{
			this.record = record;
		}

		public static explicit operator Record(RecordViewModel viewModel)
		{
			return viewModel.record;
		}
	}
}
