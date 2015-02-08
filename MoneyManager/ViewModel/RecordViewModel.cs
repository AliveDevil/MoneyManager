using GalaSoft.MvvmLight;
using MoneyManager.Model;
using System;

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

		public Tag Tag
		{
			get
			{
				return record.Tag;
			}
			set
			{
				record.Tag = value;
				RaisePropertyChanged("Tag");
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

		public float Value
		{
			get
			{
				return record.Value;
			}
			set
			{
				record.Value = value;
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
