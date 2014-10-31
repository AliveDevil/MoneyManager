using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyManager.Model
{
	public class MoneyRecord
	{
		public DateTime Date { get; set; }
		public Decimal Amount { get; set; }
		public string Description { get; set; }

		public MoneyRecord()
		{
			Date = DateTime.Now.Date;
			Amount = 0;
			Description = "Got money.";
		}
	}
}
