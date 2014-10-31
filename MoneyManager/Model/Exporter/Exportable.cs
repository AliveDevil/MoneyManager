using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Model.Exporter
{
	public interface Exportable<T>
	{
		void Save(IEnumerable<T> value);
	}
}
