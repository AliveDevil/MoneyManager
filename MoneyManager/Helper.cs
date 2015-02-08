using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager
{
	public static class Helper
	{
		public static T _<T>(this T t, Action<T> changes)
		{
			changes(t);
			return t;
		}
	}
}
