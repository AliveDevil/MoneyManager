using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.Model;
using MoneyManager.Properties;

namespace MoneyManager.ViewModel
{
	public class MoneyRecordViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Money.RecordRow moneyRecord;

		[DebuggerNonUserCode]
		public string Amount
		{
			get { return moneyRecord.Amount.ToString(); }
			set
			{
				moneyRecord.Amount = Decimal.Parse(value);
				OnPropertyChanged("Amount");
			}
		}

		public DateTimeOffset Date
		{
			get { return moneyRecord.Date; }
			set
			{
				moneyRecord.Date = value;
				OnPropertyChanged("Date");
			}
		}

		public string Header
		{
			get { return moneyRecord.Header; }
			set
			{
				moneyRecord.Header = value;
				OnPropertyChanged("Description");
			}
		}

		public MoneyRecordViewModel() : this(null) { }
		public MoneyRecordViewModel(Money.RecordRow record)
		{
			moneyRecord = record;
		}

		public void Reset()
		{
			moneyRecord.Amount = 0;
			moneyRecord.Header = "";
			OnPropertyChanged("Amount");
			OnPropertyChanged("Description");
		}

		public static implicit operator MoneyRecord(MoneyRecordViewModel model)
		{
			return model.moneyRecord;
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
