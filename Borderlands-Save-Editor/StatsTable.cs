using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borderlands_Save_Editor {

	public class StatsTable {
		public Int32 UnknownVariable1;
		public Int32 TotalBytesSize;
		public Int16 TotalEntries;
		public List<Stat> Stats;

		public StatsTable() {
			Stats = new List<Stat>();
		}
	}
}
