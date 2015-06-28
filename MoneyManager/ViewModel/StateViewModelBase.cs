using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.ViewModel
{
	public class StateViewModelBase : ViewModelBase
	{
		protected ViewStateManager ViewState { get; }

		public StateViewModelBase(ViewStateManager viewState)
		{
			ViewState = viewState;
		}
	}
}
