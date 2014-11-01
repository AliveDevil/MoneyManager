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

		private MoneyRecord moneyRecord;

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

		public DateTime Date
		{
			get { return moneyRecord.Date; }
			set
			{
				moneyRecord.Date = value;
				OnPropertyChanged("Date");
			}
		}

		public string Description
		{
			get { return moneyRecord.Description; }
			set
			{
				moneyRecord.Description = value;
				OnPropertyChanged("Description");
			}
		}

		public MoneyRecordViewModel() : this(new MoneyRecord()) { }
		public MoneyRecordViewModel(MoneyRecord record)
		{
			moneyRecord = record;
		}

		public void Reset()
		{
			moneyRecord.Amount = 0;
			moneyRecord.Description = "";
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
