using MoneyManager.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.ViewModel
{
	public class RecordListViewModel : MyViewModelBase
	{
		public IReactiveDerivedList<RecordViewModel> Records { get; set; }

		public RecordListViewModel()
		{
			Records = DatabaseContext.Instance.RecordSet.Local.CreateDerivedCollection(record => new RecordViewModel(record, false));
		}
	}
}
