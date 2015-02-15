using GalaSoft.MvvmLight;
using MoneyManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.ViewModel
{
	public abstract class MyViewModelBase : ViewModelBase
	{
		protected void RaiseAndSave(string propertyName)
		{
			DatabaseContext.Instance.SaveChanges();
			RaisePropertyChanged(propertyName);
		}
	}
}
